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
    private IUnityOfWork _unityOfWork;
    private IUserLogged _userLogged;
    public PostService(IPostRepository repository, IMapper mapper, 
        IUnityOfWork unityOfWork, IUserLogged userLogged)
    {
        _repository = repository; 
        _mapper = mapper;
        _unityOfWork = unityOfWork;  
        _userLogged = userLogged;
    }

    public async Task<GetPostResponseJson> CreatePostAsync(CreatePostRequestJson request)
    {
        var model = _mapper.Map<Post>(request); 
        model.CreatedAt = DateTime.Now; 
        if(request.Tags.Any())
        {
            var tags = _mapper.Map<IEnumerable<Tags>>(request.Tags); 
            model.Tags = (IList<Tags>)tags;
        } 
        var user = await _userLogged.GetUser(); 
        model.UserId = user.Id; 
        await _repository.CreateAsync(model); 
        await _unityOfWork.Commit(); 
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
            throw new BlogException("Postagem não encontrada."); 
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
        await _unityOfWork.Commit();
        return true;
    }

    public async Task<bool> UpdateAsync(int id, UpdatePostRequestJson request)
    {
        var post = await _repository.GetByIdAsync(id, true);
        if(post is null)
        {
            throw new NotFoundException(); 
        } 
        _mapper.Map(request, post); 
        await _repository.UpdateAsync(); 
        await _unityOfWork.Commit(); 
        return true;  
    }
}
