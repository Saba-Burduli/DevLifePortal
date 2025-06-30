using Domain.Repository.BugChaseRepositories;
using MediatR;

namespace Application.BugChase.ChangeSkinCommands;

public record ChangeSkinCommand(string Username, string NewSkin) : IRequest<Unit>;

public class ChangeSkinHandler : IRequestHandler<ChangeSkinCommand , Unit>
{
    private readonly IBugChaseRepository _service;
    public ChangeSkinHandler(IBugChaseRepository service) => _service = service;

    public async Task<Unit> Handle(ChangeSkinCommand request, CancellationToken cancellationToken)
    {
        await _service.ChangeCharacterSkinAsync(request.Username, request.NewSkin);
        return Unit.Value;
    }
}