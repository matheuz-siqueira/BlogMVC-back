using BlogMVC.Domain.Entities;

namespace BlogMVC.Domain.Interfaces;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetAllAsync(); 
    Task<Comment> GetByIdAsync(int id, bool tracking = true);
    Task<Comment> CreateAsync(Comment comment); 
    Task UpdateAsync(); 
    Task RemoveAsync(Comment comment); 
}
