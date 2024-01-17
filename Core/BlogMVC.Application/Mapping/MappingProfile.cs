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
        EntityToRequest(); 
    }

    private void RequestToEntity()
    {
        CreateMap<CreatePostRequestJson, Post>();
        CreateMap<CreateCommentRequestJson, Comment>(); 
        CreateMap<CreateTagRequestJson, Tags>();
        CreateMap<CreateAccountRequestJson, User>();  
    }

    private void EntityToRequest()
    {         
    }
}
