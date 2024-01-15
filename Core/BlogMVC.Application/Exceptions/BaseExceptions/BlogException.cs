namespace BlogMVC.Application.Exceptions.BaseExceptions;

public class BlogException : ApplicationException
{
    public BlogException(string message) : base (message)
    { }
}
