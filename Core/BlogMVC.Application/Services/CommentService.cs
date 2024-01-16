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
    private readonly IMapper _mapper;
    public CommentService(ICommentRepository commentRepository, 
        IPostRepository postRepository, 
        IMapper mapper)
    {
        _commentRepository = commentRepository; 
        _postRepository = postRepository;
        _mapper = mapper; 
    }
    public async Task<GetCommentsResponseJson> Create(CreateCommentRequestJson request, int postId)
    {
        var post = await _postRepository.GetByIdAsync(postId, false); 
        if(post is null)
        {
            throw new NotFoundException("Post não encontrado"); 
        }        
        var comment = new Comment(request.Commentary, postId);
        await _commentRepository.CreateAsync(comment); 
        var response = _mapper.Map<GetCommentsResponseJson>(comment); 
        return response;  
    }
}
