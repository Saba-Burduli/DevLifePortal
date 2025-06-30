using Domain.Repository.BugChaseRepositories;
using MediatR;

namespace Application.BugChase.UnlockAchievementCommands;

    public record UnlockAchievementCommand(string Username, string Achievement) : IRequest<Unit>;

    public class UnlockAchievementHandler : IRequestHandler<UnlockAchievementCommand ,Unit>
    {
        private readonly IBugChaseRepository _service;
        
        public UnlockAchievementHandler(IBugChaseRepository service) => _service = service;

        public async Task<Unit> Handle(UnlockAchievementCommand request, CancellationToken cancellationToken)
        {
            await _service.UnlockAchievementAsync(request.Username, request.Achievement);
            return Unit.Value;
        }
    }
