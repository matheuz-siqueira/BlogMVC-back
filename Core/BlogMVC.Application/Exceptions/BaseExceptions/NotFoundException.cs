namespace BlogMVC.Application.Exceptions.BaseExceptions;

public class NotFoundException : BlogException
{
    public NotFoundException() : base ("Recurso não encontrado") 
    {}
}
