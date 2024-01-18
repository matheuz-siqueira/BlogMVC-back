using BlogMVC.Application.Interfaces;
using BlogMVC.Application.Token;
using BlogMVC.Domain.Entities;
using BlogMVC.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BlogMVC.Application.Services;

public class UserLogged : IUserLogged
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TokenService _tokenService;
    private readonly IUserRepository _userRepository;
    public UserLogged(IHttpContextAccessor httpContextAccessor, TokenService tokenService, 
        IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenService = tokenService; 
        _userRepository = userRepository;
    }

    public async Task<User> GetUser()
    {
        var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
        var token = authorization["Bearer".Length..].Trim(); 
        var emailUser = _tokenService.GetEmail(token);
        var user = await _userRepository.GetByEmailAsync(emailUser); 
        return user; 
    }
}
