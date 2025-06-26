namespace Domain.Exceptions;

public class QuestionNotFoundException : Exception
{
    public QuestionNotFoundException() : base("Question Not Found")
    {
        
    }
}