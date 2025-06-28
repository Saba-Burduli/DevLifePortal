using System.Text.Json;
using Domain.Entities.EscapeMeetingEntities;
using Domain.Repository.EscapeMeetingRepositories;
using Infrastructure.Persistence.Configuration.EscapeMeetingConfiguration;
using StackExchange.Redis;
using IDatabase = Microsoft.EntityFrameworkCore.Storage.IDatabase;

namespace Infrastructure.Persistence.Repositories.EscapeMeetingRepositories;

public class EscapeMeetingRepository  : IEscapeMeetingRepository
{
    private readonly IDatabase _redis;
    private readonly string _favoritesKey = "escape:favorites";
    public EscapeMeetingRepository(IConnectionMultiplexer redis)
    {
        _redis = redis.GetDatabase();
    }

    public EscapeExcuse GenerateExcuse(string category, string type)
    {
        if (!ExcuseTemplates.Templates.ContainsKey(type))
            throw new ArgumentException("Unknown excuse type");

        var templates = ExcuseTemplates.Templates[type];
        var selected = templates[Random.Shared.Next(templates.Count)];

        return new EscapeExcuse
        {
            Category = category,
            Type = type,
            Text = selected,
            ConvincibilityScore = Random.Shared.Next(40, 100)
        };
    }

    public async Task SaveFavoriteAsync(EscapeExcuse excuse)
    {
        var json = JsonSerializer.Serialize(excuse);
        await _redis.ListLeftPushAsync(_favoritesKey, json);
    }

    public async Task<List<EscapeExcuse>> GetFavoritesAsync()
    {
        var values = await _redis.ListRangeAsync(_favoritesKey);
        return values
            .Select(v => JsonSerializer.Deserialize<EscapeExcuse>(v!)!)
            .ToList();
    }
}