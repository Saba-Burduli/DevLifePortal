using Application.Quizzes.Commands;
using Domain.Repository;
using MediatR;

namespace Application.Quizzes.CommandHendlers;

internal sealed class DeleteQuizHandler : IRequestHandler<DeleteQuiz>
{
    private readonly IQuizRepository _quizRepository;
    
    public DeleteQuizHandler(IQuizRepository quizRepository)
    {
        _quizRepository = quizRepository;
    }
    
    public async Task Handle(DeleteQuiz request, CancellationToken cancellationToken)
    {
        await _quizRepository.DelateQuiz(request.QuizId);
    }
}