using Domain.Entities.DevDatingEntities;
using Domain.Repository.DevDatingRepositories;
using MediatR;

namespace Application.DevDating.GetMatchesQueries;

public record GetMatchesQuery(string Username) : IRequest<List<DevProfile>>;

public class GetMatchesHandler : IRequestHandler<GetMatchesQuery, List<DevProfile>>
{
    private readonly IDevDatingRepository _service;
    public GetMatchesHandler(IDevDatingRepository service) => _service = service;

    public async Task<List<DevProfile>> Handle(GetMatchesQuery request, CancellationToken cancellationToken)
        => await _service.GetPotentialMatchesAsync(request.Username);
}