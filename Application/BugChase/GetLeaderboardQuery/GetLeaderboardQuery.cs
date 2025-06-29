using Domain.Entities.BugChaseEntities;
using Domain.Repository.BugChaseRepositories;
using MediatR;

namespace Application.BugChase.GetLeaderboardQuery;

public record GetLeaderboardQuery() : IRequest<List<LeaderboardEntry>>;

public class GetLeaderboardHandler : IRequestHandler<GetLeaderboardQuery, List<LeaderboardEntry>>
{
    private readonly IBugChaseRepository _service;
    public GetLeaderboardHandler(IBugChaseRepository service) => _service = service;

    public async Task<List<LeaderboardEntry>> Handle(GetLeaderboardQuery request, CancellationToken cancellationToken)
        => await _service.GetTopScoresAsync();
}