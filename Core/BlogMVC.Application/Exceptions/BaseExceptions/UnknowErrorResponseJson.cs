namespace BlogMVC.Application.Exceptions.BaseExceptions;

public class UnknowErrorResponseJson
{
    public List<string> ErrorMessages { get; set; }
    public UnknowErrorResponseJson(string message)
    {
        ErrorMessages = new List<string>
        {
            message
        }; 
    }
    public UnknowErrorResponseJson(List<string> messages)
    {
        ErrorMessages = messages;
    }
}
