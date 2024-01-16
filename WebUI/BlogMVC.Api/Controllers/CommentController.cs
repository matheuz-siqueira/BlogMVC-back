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
        catch(NotFoundException e)
        {
            return NotFound(new { message = e.Message }); 
        }
         
    }
    
}
