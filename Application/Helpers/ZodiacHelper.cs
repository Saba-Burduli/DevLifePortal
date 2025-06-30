namespace Application.Helpers;

public static class ZodiacHelper
{
    public static string GetZodiacSign(DateOnly dob)
    {
        var (day, month) = (dob.Day, dob.Month);

        return (month, day) switch
        {
            (1, <= 19) or (12, >= 22) => "Capricorn",
            (1, _) or (2, <= 18) => "Aquarius",
            (2, _) or (3, <= 20) => "Pisces",
            (3, _) or (4, <= 19) => "Aries",
            (4, _) or (5, <= 20) => "Taurus",
            (5, _) or (6, <= 20) => "Gemini",
            (6, _) or (7, <= 22) => "Cancer",
            (7, _) or (8, <= 22) => "Leo",
            (8, _) or (9, <= 22) => "Virgo",
            (9, _) or (10, <= 22) => "Libra",
            (10, _) or (11, <= 21) => "Scorpio",
            (11, _) or (12, <= 21) => "Sagittarius",
            _ => "Unknown"
        };
    }
}
