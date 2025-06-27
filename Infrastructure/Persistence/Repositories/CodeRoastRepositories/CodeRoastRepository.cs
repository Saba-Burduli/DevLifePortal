using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Domain.Entities.CodeRoastEntities;
using Domain.Exceptions.CodeRoastExceptions;
using Domain.Repository.CodeRoastRepositories;
using Infrastructure.CodeRoastDbContext;
using Infrastructure.Persistence.Configuration.CodeRoastConfiguration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories.CodeRoastRepositories;

public class CodeRoastRepository : ICodeRoastRepository
{
    private readonly HttpClient _httpClient;
    private readonly OpenAiSettings _settings;
    private readonly IMongoCollection<RoastLog> _cache;
    private readonly IHubContext<CodeRoastHub> _hub;

    public CodeRoastRepository(HttpClient httpClient, IOptions<OpenAiSettings> settings, IMongoClient mongoClient, IHubContext<CodeRoastHub> hub)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        var db = mongoClient.GetDatabase("CodeRoastDb");
        _cache = db.GetCollection<RoastLog>("RoastCache");
        _hub = hub;
    }

    public async Task<CodeRoastResponse> RoastCodeAsync(CodeRoastRequest request)
    {
        var cached = await _cache.Find(x => x.CodeSnippet == request.CodeSnippet).FirstOrDefaultAsync();
        if (cached != null)
        {
            await _hub.Clients.All.SendAsync("ReceiveRoast", cached.RoastMessage);
            return new CodeRoastResponse(cached.Verdict, cached.RoastMessage);
        }

        var content = new
        {
            model = "gpt-4",
            messages = new[] {
                new { role = "system", content = "You are a sarcastic senior developer who gives feedback in a roasting manner." },
                new { role = "user", content = $"Roast this code:\n{request.CodeSnippet}" }
            }
        };

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, _settings.Endpoint)
        {
            Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json")
        };
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _settings.ApiKey);

        var response = await _httpClient.SendAsync(httpRequest);
        if (!response.IsSuccessStatusCode)
            throw new CodeRoastException("Failed to contact OpenAI");

        using var responseStream = await response.Content.ReadAsStreamAsync();
        var json = await JsonDocument.ParseAsync(responseStream);
        var roast = json.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

        var result = new CodeRoastResponse("🔥 Brutal honesty incoming", roast ?? "No roast available");

        await _cache.InsertOneAsync(new RoastLog
        {
            CodeSnippet = request.CodeSnippet,
            Verdict = result.Verdict,
            RoastMessage = result.RoastMessage,
            CreatedAt = DateTime.UtcNow
        });

        await _hub.Clients.All.SendAsync("ReceiveRoast", result.RoastMessage);
        return result;
    }
}
