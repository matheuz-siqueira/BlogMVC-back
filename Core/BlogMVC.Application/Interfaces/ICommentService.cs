using BlogMVC.Application.Dtos.Comment;

namespace BlogMVC.Application.Interfaces;

public interface ICommentService
{
    Task<GetCommentsResponseJson> Create(CreateCommentRequestJson request, int postId);   
}
