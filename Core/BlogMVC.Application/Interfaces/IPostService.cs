using BlogMVC.Application.Dtos.Post;

namespace BlogMVC.Application.Interfaces;

public interface IPostService
{
    Task<GetPostResponseJson> CreatePostAsync(CreatePostRequestJson request);
    Task<IEnumerable<GetPostsResponseJson>> GetAllAsync(); 
    Task<GetPostResponseJson> GetByIdAsync(int id); 
    Task<bool> RemoveAsync(int id); 
}
