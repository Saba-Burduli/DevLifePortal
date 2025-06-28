namespace Infrastructure.Persistence.Configuration.EscapeMeetingConfiguration;

public static class ExcuseTemplates
{
    public static readonly Dictionary<string, List<string>> Templates = new()
    {
        ["Technical"] = new()
        {
            "The server spontaneously combusted.",
            "Someone deployed to production — again.",
            "CI/CD pipeline is now CI/See You Never."
        },
        ["Personal"] = new()
        {
            "My cat deployed itself into the production room.",
            "Grandma is speedrunning hospital visits.",
            "My keyboard caught feelings and is ghosting me."
        },
        ["Creative"] = new()
        {
            "The AI has become self-aware and I'm negotiating peace.",
            "Aliens abducted my Jira board.",
            "A time traveler warned me about this meeting."
        }
    };
}
