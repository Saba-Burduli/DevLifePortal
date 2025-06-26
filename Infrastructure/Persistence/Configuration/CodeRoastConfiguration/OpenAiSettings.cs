namespace Infrastructure.Persistence.Configuration.CodeRoastConfiguration;

public class OpenAiSettings
{
    public string ApiKey { get; set; } = default!;
    public string Endpoint { get; set; } = "https://api.openai.com/v1/chat/completions";
}
