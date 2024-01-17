using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Conventions;
using BlogMVC.Application.Dtos.Auth;
using BlogMVC.Application.Exceptions.BaseExceptions;
using BlogMVC.Application.Exceptions.ValidatorsExceptions;
using BlogMVC.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BlogMVC.Api.Controllers;

[Route("api/login")]
public class LoginController : BlogController
{
    private readonly ILoginService _loginService;
    private readonly IValidator<LoginRequestJson> _loginValidator;
    public LoginController(ILoginService loginService, IValidator<LoginRequestJson> loginValidator)
    {
        _loginService = loginService; 
        _loginValidator = loginValidator;
    }

    [HttpPost]
    public async Task<ActionResult<LoginResponseJson>> Login(LoginRequestJson request)
    {
        var result = _loginValidator.Validate(request); 
        if(!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        }
        try 
        {
            var response = await _loginService.Login(request); 
            return Ok(response); 
        }
        catch(InvalidLoginException e)
        {
            return StatusCode(401, new { message = e.Message });
        }
    }
}
