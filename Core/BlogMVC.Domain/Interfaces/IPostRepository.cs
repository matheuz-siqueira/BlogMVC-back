using BlogMVC.Domain.Entities;

namespace BlogMVC.Domain.Interfaces;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllAsync(); 
    Task<Post> GetByIdAsync(int id);
    Task<Post> GetByIdTrackingAsync(int id, int userId); 
    Task<Post> CreateAsync(Post post); 
    Task UpdateAsync(); 
    Task RemoveAsync(Post post) ; 
}
