using BlogMVC.Api.Filter;
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
    [ProducesResponseType(typeof(GetPostResponseJson), StatusCodes.Status201Created)]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<ActionResult> Create(CreatePostRequestJson request) 
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
    [ProducesResponseType(typeof(GetPostResponseJson), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetById(int id) 
    {
        if(id <= 0) 
        {
            return BadRequest(new { message = "Id inválido" });
        }
        try 
        {
            var response = await _postService.GetByIdAsync(id); 
            return Ok(response);  
        }
        catch(BlogException e)
        {
            return NotFound(new { message = e.Message }); 
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetPostResponseJson), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetPostsResponseJson>> GetAll()
    {
        var response = await _postService.GetAllAsync(); 
        if(response.Any())
        {
            return Ok(response); 
        }
        return NoContent(); 
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<ActionResult<bool>> Update(int id, UpdatePostRequestJson request)
    {
        if(id <= 0)
        {
            return BadRequest(new { message = "Id inválido"});
        }
        try 
        {
            var response = await _postService.UpdateAsync(id, request); 
            return Ok(response);
        }
        catch
        {
            return Ok(false); 
        }
         
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<ActionResult<bool>> Remove(int id)
    {
        var response = await _postService.RemoveAsync(id); 
        return Ok(response); 
    }
    
}
