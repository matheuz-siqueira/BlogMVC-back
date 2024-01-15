using BlogMVC.Application.Dtos.Post;
using BlogMVC.Application.Exceptions.BaseExceptions;
using BlogMVC.Application.Exceptions.ValidatorsExceptions;
using BlogMVC.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BlogMVC.Api.Controllers;

[Route("api/post")] 
public class PostController : BlogController
{
    private readonly IPostService _postService; 
    private readonly IValidator<CreatePostRequestJson> _createPostValidator;
    public PostController(IPostService postService, IValidator<CreatePostRequestJson> createPostValidator)
    {
        _postService = postService; 
        _createPostValidator = createPostValidator ;
    }


    [HttpPost]
    public async Task<ActionResult<GetPostResponseJson>> Create(CreatePostRequestJson request) 
    {
        var result = _createPostValidator.Validate(request); 
        if(!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        }
        var response = await _postService.CreatePostAsync(request); 
        return CreatedAtAction(nameof(GetById), new {id = response.Id}, response);  
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetPostResponseJson>> GetById(int id) 
    {
        try 
        {
            var response = await _postService.GetByIdAsync(id); 
            return Ok(response);  
        }
        catch(NotFoundException e)
        {
            return NotFound(new { message = e.Message }); 
        }
    }

    [HttpGet]
    public async Task<ActionResult<GetPostsResponseJson>> GetAll()
    {
        var response = await _postService.GetAllAsync(); 
        if(response.Any())
        {
            return Ok(response); 
        }
        return NoContent(); 
    }
}