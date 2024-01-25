using BlogMVC.Application.Dtos.Post;
using BlogMVC.Domain.Entities;
using BlogMVC.Domain.Pagination;

namespace BlogMVC.Application.Interfaces;

public interface IPostService
{
    Task<GetPostResponseJson> CreatePostAsync(CreatePostRequestJson request);
    PagedList<Post> GetAll(PaginationParameters parameters);
    Task<IEnumerable<GetPostResponseJson>> GetAllOfUser(); 
    Task<GetPostResponseJson> GetByIdAsync(int id); 
    Task<bool> RemoveAsync(int id); 
    Task<bool> UpdateAsync(int id, UpdatePostRequestJson request); 
}
