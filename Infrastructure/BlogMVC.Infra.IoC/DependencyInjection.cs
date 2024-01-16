using BlogMVC.Application.Dtos.Comment;
using BlogMVC.Application.Dtos.Post;
using BlogMVC.Application.Interfaces;
using BlogMVC.Application.Mapping;
using BlogMVC.Application.Services;
using BlogMVC.Application.Validators;
using BlogMVC.Domain.Interfaces;
using BlogMVC.Infra.Data.Context;
using BlogMVC.Infra.Data.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogMVC.Infra.IoC;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        AddContext(services, configuration);
        AddRepositories(services); 
        AddServices(services); 
        AddMapper(services); 
        AddValidators(services); 
    }

    private static void AddContext(this IServiceCollection services, 
        IConfiguration configuration)
    {
         services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                )
            );
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPostRepository, PostRepository>(); 
        services.AddScoped<ICommentRepository, CommentRepository>(); 
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ICommentService, CommentService>();
    }

    private static void AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile)); 
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreatePostRequestJson>, CreatePostValidator>();
        services.AddScoped<IValidator<UpdatePostRequestJson>, UpdatePostValidator>();  
        services.AddScoped<IValidator<CreateCommentRequestJson>, CreateCommentValidator>();
    }

}
