using BlogMVC.Application.Dtos.Comment;
using BlogMVC.Application.Exceptions.BaseExceptions;
using BlogMVC.Application.Exceptions.ValidatorsExceptions;
using BlogMVC.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BlogMVC.Api.Controllers;

[Route("api/comment")]
public class CommentController : BlogController
{
    private readonly ICommentService _service; 
    private readonly IValidator<CreateCommentRequestJson> _createCommentValidator;
    public CommentController(ICommentService service, 
        IValidator<CreateCommentRequestJson> createCommentValidator)
    {
        _service = service; 
        _createCommentValidator = createCommentValidator; 
    }

    [HttpPost("{id:int}")]
    public async Task<ActionResult<GetCommentsResponseJson>> Create(
            CreateCommentRequestJson request, int id)
    {
        var result = _createCommentValidator.Validate(request); 
        if(!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure()); 
        }
        try 
        {
            var response = await _service.Create(request, id); 
            return StatusCode(201, response);
        }
        catch(BlogException e)
        {
            return NotFound(new { message = e.Message }); 
        }
         
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetCommentsResponseJson>> GetById(int id)
    {
        if(id < 0)
        {
            return BadRequest(new {message = "Id inválido"}); 
        }
        try 
        {
            var response = await _service.GetById(id); 
            return Ok(response);
        }
        catch(BlogException e)
        {
            return NotFound(new { message = e.Message }); 
        }
         
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<bool>> Update(CreateCommentRequestJson request, int id)
    {
        var result = _createCommentValidator.Validate(request); 
        if(!result.IsValid)
            return BadRequest(result.Errors.ToCustomValidationFailure());
        if(id < 0)
            return BadRequest(new {message = "Comentário não encontrado"}); 
        try 
        {
            var response = await _service.Update(request, id); 
            return Ok(response); 
        }
        catch
        {
            return BadRequest(false);
        }
             
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> Remove(int id)
    {
        try
        {
            var response = await _service.Remove(id); 
            return Ok(response); 
        }
        catch
        {
            return BadRequest(false);
        }
    }    
}
