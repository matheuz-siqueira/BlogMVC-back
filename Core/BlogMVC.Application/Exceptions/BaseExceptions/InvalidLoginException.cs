namespace BlogMVC.Application.Exceptions.BaseExceptions;

public class InvalidLoginException : BlogException
{
    public InvalidLoginException() : base("Email e/ou senha incorretos") {}
}
