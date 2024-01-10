using BlogMVC.Domain.Interfaces;
using BlogMVC.Infra.Data.Context;
using BlogMVC.Infra.Data.Repositories;
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

}
