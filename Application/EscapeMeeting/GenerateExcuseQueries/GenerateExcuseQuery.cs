using Domain.Entities.EscapeMeetingEntities;
using Domain.Repository.EscapeMeetingRepositories;
using MediatR;

namespace Application.EscapeMeeting.GenerateExcuseQueries;

public record GenerateExcuseQuery(string Category, string Type) : IRequest<EscapeExcuse>;

public class GenerateExcuseHandler : IRequestHandler<GenerateExcuseQuery, EscapeExcuse>
{
    private readonly IEscapeMeetingRepository _service;
    public GenerateExcuseHandler(IEscapeMeetingRepository service) => _service = service;

    public Task<EscapeExcuse> Handle(GenerateExcuseQuery request, CancellationToken cancellationToken)
        => Task.FromResult(_service.GenerateExcuse(request.Category, request.Type));
}
