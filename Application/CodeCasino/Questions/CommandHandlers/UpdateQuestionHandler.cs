using Application.Questions.Commands;
using Application.Quizzes.Commands;
using Domain.Entities.Questions;
using Domain.Repository;
using MediatR;

namespace Application.Questions.CommandHandlers;

internal sealed class UpdateQuestionHandler : IRequestHandler<UpdateQuestion, Question>
{
    private readonly IQuestionRepository _questionRepository;
    
    public UpdateQuestionHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }
    
    public async Task<Question> Handle(UpdateQuestion request, CancellationToken cancellationToken)
    {
        return await _questionRepository.UpdateQuestion(request.Id, request.Prompt, request.Answer);
    }
}