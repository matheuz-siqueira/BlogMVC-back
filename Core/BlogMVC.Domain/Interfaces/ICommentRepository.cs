using BlogMVC.Domain.Entities;

namespace BlogMVC.Domain.Interfaces;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetAllAsync(); 
    Task<Comment> GetByIdAsync(int id);
    Task<Comment> GetByIdTrackingAsync(int id); 
    Task<Comment> CreateAsync(Comment comment); 
    Task UpdateAsync(); 
    Task RemoveAsync(Comment comment); 
}
