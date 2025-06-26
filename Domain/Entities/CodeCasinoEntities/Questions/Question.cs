using System.Security.AccessControl;
using System.Text.Json.Serialization;
using Domain.Entities.Quizzes;
using Domain.Models;

namespace Domain.Entities.Questions;

public sealed class Question : Entity<QuestionId>
{
    public QuestionId Id { get; set; }
    public string? Prompt { get; set; }
    public string? Answer { get; set; }
    
    [JsonIgnore]
    public ICollection<Quiz> Quizzes { get; set; }
}