namespace BlogMVC.Application.Exceptions.BaseExceptions;

public class IncorretPasswordException : BlogException
{
    public IncorretPasswordException() : base("Senha incorreta")
    {}
}
