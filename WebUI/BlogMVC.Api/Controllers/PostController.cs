using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using BlogMVC.Api.Filter;
using BlogMVC.Application.Dtos.Post;
using BlogMVC.Application.Exceptions.BaseExceptions;
using BlogMVC.Application.Exceptions.ValidatorsExceptions;
using BlogMVC.Application.Interfaces;
using BlogMVC.Domain.Entities;
using BlogMVC.Domain.Pagination;
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
    [ProducesResponseType(typeof(PagedList<Post>), StatusCodes.Status200OK)]
    public ActionResult GetAll([FromQuery] PaginationParameters parameters, 
        [FromServices] IMapper _mapper)
    {
        var posts = _postService.GetAll(parameters); 
        if(!posts.Any())
            return NoContent();

        var metadata = new 
        {
            posts.TotalCount, 
            posts.PageSize, 
            posts.CurrentPage, 
            posts.TotalPages,
            posts.HasNext, 
            posts.HasPrevious
        };
        
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
        var response = _mapper.Map<List<GetPostResponseJson>>(posts); 
        return Ok(response); 

    }

    [HttpGet("all-user")] 
    [ProducesResponseType(typeof(GetPostsResponseJson), StatusCodes.Status200OK)]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<ActionResult> GetAllUser()
    {
        var response = await _postService.GetAllOfUser(); 
        if(!response.Any())
            return NoContent(); 
        
        return Ok(response);  
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
