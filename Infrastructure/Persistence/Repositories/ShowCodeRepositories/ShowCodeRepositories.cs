using System.Net.Http.Headers;
using System.Net.Http.Json;
using Domain.Entities.ShowCodeEntities;
using Domain.Repository.ShowCodeRepositories;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence.Repositories.ShowCodeRepositories;

public class ShowCodeRepository : IShowCodeRepository
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ShowCodeRepository> _logger;

    public ShowCodeRepository(HttpClient httpClient, ILogger<ShowCodeRepository> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<CodeProfile> AnalyzeRepositoryAsync(string accessToken, string repoOwner, string repoName)
    {
        var query = "query RepositoryInfo {" +
                    " repository(owner: \"" + repoOwner + "\", name: \"" + repoName + "\") {" +
                    " defaultBranchRef {" +
                    " target {" +
                    " ... on Commit {" +
                    " history(first: 50) {" +
                    " nodes {" +
                    " message" +
                    " committedDate" +
                    " }" +
                    " }" +
                    " }" +
                    " }" +
                    " }" +
                    " }" +
                    "}";

        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.github.com/graphql")
        {
            Content = JsonContent.Create(new { query })
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.UserAgent.Add(new ProductInfoHeaderValue("DevLife", "1.0"));

        var response = await _httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("GitHub API error: {Content}", content);
            throw new Exception("GitHub API error: " + content);
        }

        // 🔍 Mock analysis (replace with actual analysis logic)
        var profile = new CodeProfile
        {
            Username = repoOwner,
            RepoName = repoName,
            PersonalityType = "You are a Chaotic Debugger",
            Strengths = "Bold refactors, rapid delivery",
            Weaknesses = "Scattered naming, zero comments",
            CelebrityMatch = "Linus Torvalds",
            ShareableImageUrl = await GenerateCardImage("Chaotic Debugger")
        };

        return profile;
    }

    private async Task<string> GenerateCardImage(string personalityType)
    {
        await Task.Delay(150);
        return $"https://cdn.devlife/cards/{personalityType.Replace(" ", "_").ToLower()}.png";
    }
}