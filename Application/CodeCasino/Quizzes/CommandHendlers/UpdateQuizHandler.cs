using Application.Quizzes.Commands;
using Domain.Entities.Quizzes;
using Domain.Repository;
using MediatR;

namespace Application.Quizzes.CommandHendlers;

internal sealed class UpdateQuizHandler : IRequestHandler<UpdateQuiz, Quiz>
{
    private readonly IQuizRepository _quizRepository;
    
    public UpdateQuizHandler(IQuizRepository quizRepository)
    {
        _quizRepository = quizRepository;
    }

    public async Task<Quiz> Handle(UpdateQuiz request, CancellationToken cancellationToken)
    {
        return await _quizRepository.UpdateQuiz(request.Id, request.Title, request.QuestionIds);
    }
}