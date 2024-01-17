using BlogMVC.Application.Exceptions.BaseExceptions;
using BlogMVC.Application.Token;
using BlogMVC.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace BlogMVC.Api.Filter;

public class AuthenticatedUserAttribute : AuthorizeAttribute,  IAsyncAuthorizationFilter
{
    private readonly TokenService _tokenService;
    private readonly IUserRepository _userRepository;
    public AuthenticatedUserAttribute(TokenService tokenService, IUserRepository userRepository)
    {
        _tokenService = tokenService; 
        _userRepository = userRepository;
    } 
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenInRequest(context);
            var email = _tokenService.GetEmail(token); 
            var user = await _userRepository.GetByEmailAsync(email); 
            if(user is null)
            {
                throw new Exception();
            }
        }
        catch(SecurityTokenExpiredException)
        {
            ExpiredToken(context);
        }
        catch
        {
            AccessDenied(context);
        }
        
    }

    private string TokenInRequest(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();
        if(string.IsNullOrWhiteSpace(authorization))
            throw new Exception();
        return authorization["Bearer".Length..].Trim(); 
    }

    private void ExpiredToken(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new ErrorResponseJson("Faça login novamente"));
    }

    private void AccessDenied(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(
                new ErrorResponseJson("Você não tem permissão para acessar este recurso"));
    }
}
