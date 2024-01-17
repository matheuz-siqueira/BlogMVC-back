using BlogMVC.Api.Filter;
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
    private readonly IValidator<UpdatePasswordRequestJson> _updatePasswordValidator;
    public UserController(IUserService userService, 
        IValidator<CreateAccountRequestJson> createAccountValidator, 
        IValidator<UpdatePasswordRequestJson> updatePasswordValidator)
    {
        _userService = userService; 
        _createAccountValidator = createAccountValidator;
        _updatePasswordValidator = updatePasswordValidator;  
    }

    [HttpPost]
    public async Task<ActionResult<TokenResponseJson>> Register(
        CreateAccountRequestJson request)
    {
        var result = _createAccountValidator.Validate(request); 
        if(!result.IsValid)
            return BadRequest(result.Errors.ToCustomValidationFailure()); 
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

    [HttpPut("update-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<ActionResult> UpdatePassword(UpdatePasswordRequestJson request)
    {
        var result = _updatePasswordValidator.Validate(request); 
        if(!result.IsValid)
            return BadRequest(result.Errors.ToCustomValidationFailure());
        try 
        {
            await _userService.UpdatePassword(request); 
            return NoContent();
        }
        catch(IncorretPasswordException e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}
