using BlogMVC.Domain.Entities;
using BlogMVC.Domain.Pagination;

namespace BlogMVC.Domain.Interfaces;

public interface IPostRepository
{
    PagedList<Post> GetAll(PaginationParameters parameters);
    Task<Post> GetByIdAsync(int id);
    Task<Post> GetByIdTrackingAsync(int id, int userId); 
    Task<Post> CreateAsync(Post post); 
    Task UpdateAsync(); 
    Task RemoveAsync(Post post) ; 
}
