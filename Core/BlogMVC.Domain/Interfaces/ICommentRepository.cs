using BlogMVC.Domain.Entities;

namespace BlogMVC.Domain.Interfaces;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetCommentsAsync(); 
    Task<Comment> GetCommentAsync(int id);
    Task<Comment> CreateAsync(Comment comment); 
    Task UpdateAsync(Comment comment); 
    Task RemoveAsync(int id); 
}
