using Application.Questions.Queries;
using Domain.Entities.Questions;
using Domain.Repository;
using MediatR;

namespace Application.Questions.QueryHandlers;

internal sealed class GetAllQuestionsHandler : IRequestHandler<GetAllQuestions, ICollection<Question>>
{
    private readonly IQuestionRepository _questionRepository;
    
    public GetAllQuestionsHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }
    
    public async Task<ICollection<Question>> Handle(GetAllQuestions request, CancellationToken cancellationToken)
    {
        return await _questionRepository.GetAllQuestion(request.SortColumn, request.SortOrder, request.Page, request.PageSize, request.Prompt, request.QuizId);
    }
}