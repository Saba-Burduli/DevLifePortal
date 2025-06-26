using Application.Quizzes.Queries;
using Domain.Entities.Quizzes;
using Domain.Repository;
using MediatR;

namespace Application.Quizzes.QueryHendlers;

internal sealed class GetQuizByIdHandler : IRequestHandler<GetQuizById, Quiz>
{
    private readonly IQuizRepository _quizRepository;
    
    public GetQuizByIdHandler(IQuizRepository quizRepository)
    {
        _quizRepository = quizRepository;
    }

    public async Task<Quiz> Handle(GetQuizById request, CancellationToken cancellationToken)
    {
        return await _quizRepository.GetQuizById(request.QuizId);
    }
}