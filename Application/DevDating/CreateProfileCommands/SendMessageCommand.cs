using Domain.Repository.DevDatingRepositories;
using MediatR;

namespace Application.DevDating.CreateProfileCommands;

public record SendMessageCommand(string FromUser, string ToUser, string Message) : IRequest<string>;

public class SendMessageHandler : IRequestHandler<SendMessageCommand, string>
{
    private readonly IDevDatingRepository _service;
    public SendMessageHandler(IDevDatingRepository service) => _service = service;

    public async Task<string> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        => await _service.GetAIResponseAsync(request.FromUser, request.ToUser, request.Message);
}