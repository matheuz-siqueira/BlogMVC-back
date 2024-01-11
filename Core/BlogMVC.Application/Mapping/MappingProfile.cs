using AutoMapper;
using BlogMVC.Application.Dtos.Comment;
using BlogMVC.Application.Dtos.Post;
using BlogMVC.Application.Dtos.Tags;
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
    }

    private void EntityToRequest()
    {
        CreateMap<Post, GetPostsResponseJson>(); 
        CreateMap<Post, GetPostResponseJson>();
        CreateMap<Tags, GetTagsResponseJson>();
        CreateMap<Comment, GetCommentsResponseJson>();         
    }
}
