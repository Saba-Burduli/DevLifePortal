using System.Data.Common;
using System.Linq.Expressions;
using Domain.Entities.Questions;
using Domain.Entities.Quizzes;
using Domain.Exceptions;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly QuizPollDbContext _context;
    private readonly IQuestionRepository _questionRepository;

    public QuizRepository(QuizPollDbContext context)
    {
        _context = context;
    }
public QuizRepository(QuizPollDbContext context, IQuestionRepository questionRepository)
    {
        _context = context;
        _questionRepository = questionRepository;
    }
    
    public async Task<ICollection<Quiz>> GetAllQuizzes(string? sortColumn, string? sortOrder, int page, int pageSize, string? title)
    {
        IQueryable<Quiz> quizzesQuery = _context.Quizzes;

        if (!string.IsNullOrEmpty(title))
        {
            quizzesQuery = quizzesQuery.Where(q => q.Title.ToLower().Contains(title.ToLower()));
        }
        
        if (sortOrder?.ToLower() == "desc")
        {
            quizzesQuery = quizzesQuery.OrderByDescending(GetSortProperty(sortColumn));
        }
        else
        {
            quizzesQuery = quizzesQuery.OrderBy(GetSortProperty(sortColumn));
        }
        
        var quizzes = await quizzesQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return quizzes;
    }
    
    private static Expression<Func<Quiz, object>> GetSortProperty(string? sortColumn)
    {
        return sortColumn?.ToLower() switch
        {
            "title" => question => question.Title,
            "last-modified" => question => question.LastModified,
            _ => question => question.AddedTime
        };
    }

    public async Task<Quiz> GetQuizById(QuizId quizId)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Questions)
            .FirstOrDefaultAsync(q => q.Id == quizId);

        if (quiz is null) throw new QuizNotFoundException();
        
        return quiz;
    }

    public async Task<Quiz> CreateQuiz(Quiz toCreate)
    {
        toCreate.AddedTime = DateTime.Now;
        toCreate.LastModified = DateTime.Now;
        
        _context.Quizzes.Add(toCreate);
        await _context.SaveChangesAsync();

        return toCreate;
    }

    public async Task<Quiz> UpdateQuiz(QuizId quizId, string title, ICollection<QuestionId> questionIds)
    {
        var quiz = _context.Quizzes
            .Include(q => q.Questions)
            .FirstOrDefault(q => q.Id == quizId);

        if (quiz is null) throw new QuizNotFoundException(); 
        
        var questions = await _questionRepository.GetQuestionsByIds(questionIds);
        
        quiz.Title = title;
        quiz.LastModified = DateTime.Now;

        quiz.Questions.Clear();
        quiz.Questions = questions;

        await _context.SaveChangesAsync();
        
        return quiz;
    }
    
    public async Task DelateQuiz(QuizId quizId)
    {
        var quiz = _context.Quizzes.FirstOrDefault(q => q.Id == quizId);

        if (quiz is null) throw new QuizNotFoundException();

        _context.Quizzes.Remove(quiz);
        await _context.SaveChangesAsync();
    }
}