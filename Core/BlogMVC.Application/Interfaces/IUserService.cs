using BlogMVC.Application.Dtos.User;
using BlogMVC.Domain.Entities;

namespace BlogMVC.Application.Interfaces;

public interface IUserService
{
    Task<TokenResponseJson> CreateAccount(CreateAccountRequestJson request); 
    Task UpdatePassword(UpdatePasswordRequestJson request);
    Task<GetProfileResponseJson> GetProfile(); 
}
