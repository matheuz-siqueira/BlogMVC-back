using AutoMapper;
using BlogMVC.Application.Dtos.Comment;
using BlogMVC.Application.Dtos.Post;
using BlogMVC.Application.Dtos.Tags;
using BlogMVC.Application.Dtos.User;
using BlogMVC.Domain.Entities;

namespace BlogMVC.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        RequestToEntity(); 
        EntityToResponse(); 
    }

    private void RequestToEntity()
    {
        CreateMap<CreatePostRequestJson, Post>();
        CreateMap<UpdatePostRequestJson, Post>();
        CreateMap<CreateCommentRequestJson, Comment>(); 
        CreateMap<CreateAccountRequestJson, User>();  
    }

    private void EntityToResponse()
    {         
        CreateMap<User, GetProfileResponseJson>(); 
        CreateMap<Post, GetPostResponseJson>();
        CreateMap<Post, GetPostsResponseJson>();  
        CreateMap<Comment, GetCommentsResponseJson>(); 
    }
}
