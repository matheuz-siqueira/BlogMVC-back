using AutoMapper;   
using BlogMVC.Application.Dtos.Post;
using BlogMVC.Application.Exceptions.BaseExceptions;
using BlogMVC.Application.Interfaces;
using BlogMVC.Domain.Entities;
using BlogMVC.Domain.Interfaces;
using BlogMVC.Domain.Pagination;

namespace BlogMVC.Application.Services;

public class PostService : IPostService
{

    private readonly IPostRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IUserLogged _userLogged;
    public PostService(IPostRepository repository, IMapper mapper, 
        IUnityOfWork unityOfWork, IUserLogged userLogged, 
        IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository; 
        _mapper = mapper;
        _unityOfWork = unityOfWork;  
        _userLogged = userLogged;
    }

    public async Task<GetPostResponseJson> CreatePostAsync(CreatePostRequestJson request)
    {
        var model = _mapper.Map<Post>(request); 
        model.CreatedAt = DateTime.Now;
        model.UpdateAt = model.CreatedAt;  
        var user = await _userLogged.GetUser(); 
        model.UserId = user.Id; 
        await _repository.CreateAsync(model); 
        await _unityOfWork.Commit(); 
        var response = _mapper.Map<GetPostResponseJson>(model); 
        return response; 
    }

    public PagedList<Post> GetAll(PaginationParameters parameters)
    {
        return _repository.GetAll(parameters);  
    }
    
    public async Task<IEnumerable<GetPostResponseJson>> GetAllOfUser()
    {
        var user = await _userLogged.GetUser(); 
        var posts = await _repository.GetAllOfUser(user.Id); 
        var response = _mapper.Map<IEnumerable<GetPostResponseJson>>(posts); 
        return response;
    }

    public async Task<GetPostResponseJson> GetByIdAsync(int id)
    {
        var post = await _repository.GetByIdAsync(id); 
        if(post is null)
        {
            throw new NotFoundException(); 
        }
        var response = _mapper.Map<GetPostResponseJson>(post);
        foreach(var comment in response.Comments)
        {
            var user = await _userRepository.GetByIdAsync(comment.UserId); 
            comment.Author = user.Name; 
        }
        return response; 
    }
    public async Task<bool> UpdateAsync(int id, UpdatePostRequestJson request)
    {
        var user = await _userLogged.GetUser();
        var post = await _repository.GetByIdTrackingAsync(id, user.Id) ?? throw new NotFoundException();
        _mapper.Map(request, post); 
        post.UpdateAt = DateTime.Now; 
        await _repository.UpdateAsync(); 
        await _unityOfWork.Commit(); 
        return true;  
    }

    public async Task<bool> RemoveAsync(int id)
    {
        var user = await _userLogged.GetUser(); 
        var post = await _repository.GetByIdAsync(id); 
        if((post is null) || post.UserId != user.Id) 
        {
            return false; 
        }
        await _repository.RemoveAsync(post);  
        await _unityOfWork.Commit();
        return true;
    }
}
