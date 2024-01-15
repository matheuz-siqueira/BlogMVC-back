namespace BlogMVC.Application.Exceptions.BaseExceptions;

public class NotFoundException : BlogException
{
    public NotFoundException(string message ) : base (message) 
    { }
}
