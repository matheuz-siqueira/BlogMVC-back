using BlogMVC.Application.Dtos.Post;
using BlogMVC.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogMVC.Api.Controllers;

[Route("api/post")] 
public class PostController : BlogController
{
    private readonly IPostService _postService; 
    public PostController(IPostService postService)
    {
        _postService = postService; 
    }


    [HttpPost]
    public async Task<ActionResult<GetPostResponseJson>> Create(CreatePostRequestJson request) 
    {
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
        catch(Exception e)
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
