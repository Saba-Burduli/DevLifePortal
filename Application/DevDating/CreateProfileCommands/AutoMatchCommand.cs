using Domain.Entities.DevDatingEntities;
using Domain.Repository.DevDatingRepositories;
using MediatR;

namespace Application.DevDating.CreateProfileCommands;
public record AutoMatchCommand(string Username) : IRequest<DevMatch>;

public class AutoMatchHandler : IRequestHandler<AutoMatchCommand, DevMatch>
{
    private readonly IDevDatingRepository _service;
    public AutoMatchHandler(IDevDatingRepository service) => _service = service;

    public async Task<DevMatch> Handle(AutoMatchCommand request, CancellationToken cancellationToken)
        => await _service.AutoMatchAsync(request.Username);
}
