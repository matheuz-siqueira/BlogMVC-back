using BlogMVC.Domain.Entities;

namespace BlogMVC.Domain.Interfaces;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllAsync(); 
    Task<Post> GetByIdAsync(int id, bool tracking = true); 
    Task<Post> CreateAsync(Post post); 
    Task UpdateAsync(); 
    Task RemoveAsync(int id); 
}
