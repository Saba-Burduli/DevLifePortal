using Domain.Models;

namespace Domain.ValueObjects;

public class CsvQuizModel : ValueObject
{
    public int QuestionNumber { get; set; }
    public string? Question { get; set; }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return QuestionNumber;
        yield return Question;
    }
}