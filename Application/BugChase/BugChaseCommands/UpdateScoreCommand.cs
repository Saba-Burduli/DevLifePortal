using Domain.Repository.BugChaseRepositories;
using MediatR;

namespace Application.BugChase.BugChaseCommands;

public record UpdateScoreCommand(string Username, int Score) : IRequest<Unit>;

public class UpdateScoreHandler : IRequestHandler<UpdateScoreCommand , Unit>
{
    private readonly IBugChaseRepository _service;
    public UpdateScoreHandler(IBugChaseRepository service) => _service = service;

    public async Task<Unit> Handle(UpdateScoreCommand request, CancellationToken cancellationToken)
    {
        await _service.UpdateScoreAsync(request.Username, request.Score);
        return Unit.Value;
    }
}
