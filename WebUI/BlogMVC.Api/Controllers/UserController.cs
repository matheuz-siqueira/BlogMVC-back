using BlogMVC.Application.Dtos.User;
using BlogMVC.Application.Exceptions.BaseExceptions;
using BlogMVC.Application.Exceptions.ValidatorsExceptions;
using BlogMVC.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BlogMVC.Api.Controllers;

[Route("api/users")]
public class UserController : BlogController
{
    private readonly IUserService _userService;
    private readonly IValidator<CreateAccountRequestJson> _createAccountValidator;
    public UserController(IUserService userService, IValidator<CreateAccountRequestJson> createAccountValidator)
    {
        _userService = userService; 
        _createAccountValidator = createAccountValidator; 
    }

    [HttpPost]
    public async Task<ActionResult<TokenResponseJson>> Register(
        CreateAccountRequestJson request)
    {
        var result = _createAccountValidator.Validate(request); 
        if(!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure()); 
        }
        try 
        {
            var response = await _userService.CreateAccount(request); 
            return Created("", response);
        }
        catch(BlogException e)
        {
            return BadRequest(new {message = e.Message });
        }
    }
}
