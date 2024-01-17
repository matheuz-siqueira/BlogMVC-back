namespace BlogMVC.Application.Exceptions.BaseExceptions;

public class BlogException : SystemException
{
    public BlogException(string message) : base (message)
    { }
}
