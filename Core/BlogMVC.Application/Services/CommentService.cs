using AutoMapper;
using BlogMVC.Application.Dtos.Comment;
using BlogMVC.Application.Exceptions.BaseExceptions;
using BlogMVC.Application.Interfaces;
using BlogMVC.Domain.Entities;
using BlogMVC.Domain.Interfaces;

namespace BlogMVC.Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserLogged _userLogged;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IMapper _mapper;
    public CommentService(ICommentRepository commentRepository, 
        IPostRepository postRepository, 
        IMapper mapper, IUserLogged userLogged, 
        IUserRepository userRepository, 
        IUnityOfWork unityOfWork)
    {
        _commentRepository = commentRepository; 
        _postRepository = postRepository;
        _mapper = mapper; 
        _userLogged = userLogged;
        _userRepository = userRepository;
        _unityOfWork = unityOfWork; 
    }
    public async Task<GetCommentsResponseJson> Create(CreateCommentRequestJson request, int postId)
    {
        var post = await _postRepository.GetByIdAsync(postId);  
        var user = await _userLogged.GetUser();
        if(post is null)
        {
            throw new NotFoundException(); 
        }        
        var comment = _mapper.Map<Comment>(request); 
        comment.PostId = post.Id; 
        comment.UserId = user.Id;
        await _commentRepository.CreateAsync(comment); 
        var response = _mapper.Map<GetCommentsResponseJson>(comment);
        response.Author = user.Name;   
        return response;  
    }

    public async Task<GetCommentsResponseJson> GetById(int commentId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId); 
        if(comment is null)
            throw new NotFoundException(); 

        var response = _mapper.Map<GetCommentsResponseJson>(comment);
        var user = await _userRepository.GetByIdAsync(response.UserId);  
        response.Author = user.Name;  
        return response;

    }

    public async Task<bool> Remove(int commentId)
    {
        var user = await _userLogged.GetUser(); 
        var comment = await _commentRepository.GetByIdAsync(commentId); 
        if((comment is null) || comment.UserId != user.Id)
            throw new NotFoundException(); 
        
        await _commentRepository.RemoveAsync(comment); 
        await _unityOfWork.Commit(); 
        return true; 
    }

    public async Task<bool> Update(CreateCommentRequestJson request, int commentId)
    {
        var comment = await _commentRepository.GetByIdTrackingAsync(commentId);
        if(comment is null)
        {
            throw new BlogException("Comentário não encontrado"); 
        } 
        await _commentRepository.UpdateAsync(); 
        return true; 
        
    }
}
