using BlogMVC.Domain.Entities;

namespace BlogMVC.Domain.Interfaces;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetPostsAsync(); 
    Task<Post> GetPostAsync(int id); 
    Task<Post> CreateAsync(Post post); 
    Task UpdateAsync(Post post); 
    Task RemoveAsync(int id); 
}
