using BlogMVC.Application.Dtos.Auth;

namespace BlogMVC.Application.Interfaces;

public interface ILoginService
{
    Task<LoginResponseJson> Login(LoginRequestJson request);
}
