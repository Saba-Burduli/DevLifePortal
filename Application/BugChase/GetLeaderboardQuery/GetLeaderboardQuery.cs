using Domain.Entities.BugChaseEntities;
using Domain.Repository.BugChaseRepositories;
using MediatR;

namespace Application.BugChase.GetLeaderboardQuery;

public record GetLeaderboardQuery() : IRequest<List<LeaderboardEntry>>;

public class GetLeaderboardHandler : IRequestHandler<GetLeaderboardQuery, List<LeaderboardEntry>>
{
    private readonly IBugChaseRepository _repository;
    public GetLeaderboardHandler(IBugChaseRepository repository) => _repository = repository;

    public async Task<List<LeaderboardEntry>> Handle(GetLeaderboardQuery request, CancellationToken cancellationToken)
        => await _repository.GetTopScoresAsync();
}
