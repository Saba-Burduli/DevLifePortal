using DnsClient.Internal;
using Domain.Entities.DevDatingEntities;
using Domain.Repository.DevDatingRepositories;
using MongoDB.Driver;

namespace Infrastructure.DevDatingRepository;

public class DevDatingRepository : IDevDatingRepository
{
    private readonly IMongoCollection<DevProfile> _profiles;
    private readonly IMongoCollection<DevMatch> _matches;
    private readonly HttpClient _httpClient;
    public DevDatingRepository(IMongoClient mongoClient, IHttpClientFactory httpClientFactory)
    {
        var database = mongoClient.GetDatabase("DevLifePortal");
        _profiles = database.GetCollection<DevProfile>("Profiles");
        _matches = database.GetCollection<DevMatch>("Matches");
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<DevProfile> CreateProfileAsync(DevProfile profile)
    {
        await _profiles.InsertOneAsync(profile);
        return profile;
    }

    public async Task<List<DevProfile>> GetPotentialMatchesAsync(string username)
    {
        var user = await _profiles.Find(p => p.Username == username).FirstOrDefaultAsync();
        if (user == null) return [];

        return await _profiles.Find(p => p.Username != username && p.Gender == user.Preferences && !p.IsMatched).ToListAsync();
    }

    public async Task<DevMatch> AutoMatchAsync(string username)
    {
        var user = await _profiles.Find(p => p.Username == username).FirstOrDefaultAsync();
        if (user == null || user.IsMatched) throw new Exception("User not found or already matched.");

        var match = await _profiles.Find(p => p.Gender == user.Preferences && p.Preferences == user.Gender && !p.IsMatched && p.Username != username).FirstOrDefaultAsync();

        if (match == null) throw new Exception("No match found.");

        user.IsMatched = true;
        
        match.IsMatched = true;
        await _profiles.ReplaceOneAsync(p => p.Id == user.Id, user);
        await _profiles.ReplaceOneAsync(p => p.Id == match.Id, match);

        var result = new DevMatch { UserA = user.Username, UserB = match.Username };
        await _matches.InsertOneAsync(result);
        return result;
    }

    public async Task<string> GetAIResponseAsync(string fromUser, string toUser, string message)
    {
        var user = await _profiles.Find(p => p.Username == toUser).FirstOrDefaultAsync();
        var personality = user?.Personality ?? "friendly";

        await Task.Delay(300);
        return personality switch
        {
            "sarcastic" => $"[AI 🤖 to {fromUser}]: Oh really? Another '{message}' story? Go on, impress me...",
            "friendly" => $"[AI 🤖 to {fromUser}]: That's so cool! I’d love to hear more about {message}!",
            "romantic" => $"[AI 💘 to {fromUser}]: {message}? That’s so dreamy... You’re totally my type! 😍",
            _ => $"[AI 🤖 to {fromUser}]: Tell me more about {message}."
        };
    }
}