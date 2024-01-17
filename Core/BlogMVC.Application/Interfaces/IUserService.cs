using BlogMVC.Application.Dtos.User;

namespace BlogMVC.Application.Interfaces;

public interface IUserService
{
    Task<TokenResponseJson> CreateAccount(CreateAccountRequestJson request); 
}
