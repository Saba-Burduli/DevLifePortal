using Application.Questions.Commands;
using Domain.Entities.Questions;
using Domain.Repository;
using MediatR;

namespace Application.Questions.CommandHandlers;

internal sealed class CreateQuestionHandler : IRequestHandler<CreateQuestion, Question>
{
    private readonly IQuestionRepository _questionRepository;
    
    public CreateQuestionHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }
    
    public async Task<Question> Handle(CreateQuestion request, CancellationToken cancellationToken)
    {
        var question = new Question
        {
            Id = new QuestionId(Guid.NewGuid()),
            Prompt = request.Prompt,
            Answer = request.Answer,
            AddedTime = DateTime.Now,
            LastModified = DateTime.Now
        };

        return await _questionRepository.CreateQuestion(question);
    }
}