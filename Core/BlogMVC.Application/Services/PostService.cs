using AutoMapper;
using BlogMVC.Application.Dtos.Post;
using BlogMVC.Application.Exceptions.BaseExceptions;
using BlogMVC.Application.Interfaces;
using BlogMVC.Domain.Entities;
using BlogMVC.Domain.Interfaces;

namespace BlogMVC.Application.Services;

public class PostService : IPostService
{

    private readonly IPostRepository _repository;
    private IMapper _mapper;
    public PostService(IPostRepository repository, IMapper mapper)
    {
        _repository = repository; 
        _mapper = mapper; 
    }

    public async Task<GetPostResponseJson> CreatePostAsync(CreatePostRequestJson request)
    {
        var model = _mapper.Map<Post>(request); 
        model.AddDate(); 
        await _repository.CreateAsync(model); 
        var response = _mapper.Map<GetPostResponseJson>(model); 
        return response; 
    }

    public async Task<IEnumerable<GetPostsResponseJson>> GetAllAsync()
    {
        var posts = await _repository.GetAllAsync(); 
        var response = _mapper.Map<IEnumerable<GetPostsResponseJson>>(posts);
        return response;  
    }

    public async Task<GetPostResponseJson> GetByIdAsync(int id)
    {
        var post = await _repository.GetByIdAsync(id, false); 
        if(post is null)
        {
            throw new NotFoundException("Postagem não encontrada."); 
        }
        var response = _mapper.Map<GetPostResponseJson>(post); 
        return response; 
    }

    public async Task<bool> RemoveAsync(int id)
    {
        var post = await _repository.GetByIdAsync(id, false); 
        if(post is null) 
        {
            return false; 
        }
        await _repository.RemoveAsync(post);  
        return true;
    }
}
