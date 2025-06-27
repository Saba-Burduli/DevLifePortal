using Domain.Entities.DevDatingEntities;
using Domain.Repository.DevDatingRepositories;
using MediatR;

namespace Application.DevDating.CreateProfileCommands;

public record CreateProfileCommand(DevProfile Profile) : IRequest<DevProfile>;

public class CreateProfileHandler : IRequestHandler<CreateProfileCommand, DevProfile>
{
    private readonly IDevDatingRepository _service;
    public CreateProfileHandler(IDevDatingRepository service) => _service = service;

    public async Task<DevProfile> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        => await _service.CreateProfileAsync(request.Profile);
}