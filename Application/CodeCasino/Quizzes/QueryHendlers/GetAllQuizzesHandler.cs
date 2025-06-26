using Application.Quizzes.Queries;
using Domain.Entities.Quizzes;
using Domain.Repository;
using MediatR;

namespace Application.Quizzes.QueryHendlers;

internal sealed class GetAllQuizzesHandler : IRequestHandler<GetAllQuizzes, ICollection<Quiz>>
{
    private readonly IQuizRepository _quizRepository;
    
    public GetAllQuizzesHandler(IQuizRepository quizRepository)
    {
        _quizRepository = quizRepository;
    }
    
    public async Task<ICollection<Quiz>> Handle(GetAllQuizzes request, CancellationToken cancellationToken)
    {
        return await _quizRepository.GetAllQuizzes(request.SortColumn, request.SortOrder, request.Page, request.PageSize, request.Title);
    }
}