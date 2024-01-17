using BlogMVC.Domain.Entities;

namespace BlogMVC.Application.Interfaces;

public interface IUserLogged
{
    Task<User> GetUser();
}
