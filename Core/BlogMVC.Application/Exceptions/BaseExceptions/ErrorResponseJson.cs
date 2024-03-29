namespace BlogMVC.Application.Exceptions.BaseExceptions;

public class ErrorResponseJson
{
    public List<string> Messages { get; set; }
    public ErrorResponseJson(string message)
    {
        Messages = new List<string>
        {
            message
        };
    }
    public ErrorResponseJson(List<string> messages)
    {
        Messages = messages;
    }
}
