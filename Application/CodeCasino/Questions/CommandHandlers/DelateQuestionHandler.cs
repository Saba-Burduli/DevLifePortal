using Application.Questions.Commands;
using Domain.Repository;
using MediatR;

namespace Application.Questions.CommandHandlers;

internal sealed class DeleteQuestionHandler : IRequestHandler<DeleteQuestion>
{
    private readonly IQuestionRepository _questionRepository;
    
    public DeleteQuestionHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }
    
    public async Task Handle(DeleteQuestion request, CancellationToken cancellationToken)
    {
        await _questionRepository.DelateQuestion(request.QuestionId);
    }
}