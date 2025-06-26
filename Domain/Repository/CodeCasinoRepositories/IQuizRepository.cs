using Domain.Entities.Questions;
using Domain.Entities.Quizzes;

namespace Domain.Repository;

public interface IQuizRepository
{
    Task<ICollection<Quiz>> GetAllQuizzes(string? sortColumn, string sortOrder, int page, int pageSize, string? title);
    Task<Quiz> GetQuizById(QuizId quizId);
    Task<Quiz> CreateQuiz(Quiz toCreate);
    Task<Quiz> UpdateQuiz(QuizId quizId, string title ,ICollection<QuestionId> questionIds);
    Task DelateQuiz(QuizId quizId);

}