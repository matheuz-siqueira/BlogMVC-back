using BlogMVC.Application.Dtos.Auth;
using BlogMVC.Application.Exceptions.BaseExceptions;
using BlogMVC.Application.Interfaces;
using BlogMVC.Application.Token;
using BlogMVC.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BlogMVC.Application.Authentication;

public class LoginService : ILoginService
{
    private readonly TokenService _tokenService;
    private readonly IUserRepository _userRepository; 
    public LoginService(TokenService tokenService, IUserRepository userRepository)
    {
        _tokenService = tokenService; 
        _userRepository = userRepository; 
    }
    public async Task<LoginResponseJson> Login(LoginRequestJson request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email); 
        if((user is null) || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            throw new InvalidLoginException();
        }    
        var token = _tokenService.GenerateToken(user.Email);
        return new LoginResponseJson
        {

            Name = user.Name, 
            Email = user.Email,
            Token = token 
        };   
    }
}
