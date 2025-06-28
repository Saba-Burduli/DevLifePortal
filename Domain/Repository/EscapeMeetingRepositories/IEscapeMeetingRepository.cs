using Domain.Entities.EscapeMeetingEntities;

namespace Domain.Repository.EscapeMeetingRepositories;

public interface IEscapeMeetingRepository
{
    EscapeExcuse GenerateExcuse(string category, string type);
    Task SaveFavoriteAsync(EscapeExcuse excuse);
    Task<List<EscapeExcuse>> GetFavoritesAsync();
}
