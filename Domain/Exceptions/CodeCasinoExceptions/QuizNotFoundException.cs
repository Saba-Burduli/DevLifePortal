namespace Domain.Exceptions;

public class QuizNotFoundException : Exception
{
    public QuizNotFoundException() : base("Quiz not Found")
    {
        
    }
}