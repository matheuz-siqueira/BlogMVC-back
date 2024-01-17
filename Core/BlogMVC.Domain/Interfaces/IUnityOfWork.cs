namespace BlogMVC.Domain.Interfaces;

public interface IUnityOfWork
{
    Task Commit();
}
