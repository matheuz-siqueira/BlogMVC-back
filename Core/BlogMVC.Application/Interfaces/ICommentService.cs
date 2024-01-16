using BlogMVC.Application.Dtos.Comment;

namespace BlogMVC.Application.Interfaces;

public interface ICommentService
{
    Task<GetCommentsResponseJson> Create(CreateCommentRequestJson request, int postId);   
    Task<GetCommentsResponseJson> GetById(int commentId); 
    Task<bool> Update(CreateCommentRequestJson request, int commentId); 
    Task<bool> Remove(int commentId); 
}
