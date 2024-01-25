using BlogMVC.Domain.Entities;
using BlogMVC.Domain.Interfaces;
using BlogMVC.Domain.Pagination;
using BlogMVC.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogMVC.Infra.Data.Repositories;

public class PostRepository : IPostRepository
{
    private readonly AppDbContext _context;
    public PostRepository(AppDbContext context)
    {
        _context = context; 
    }
    public async Task<Post> GetByIdAsync(int id)
    {
        return  await _context.Posts.AsNoTracking().Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id); 
    }

    public async Task<Post> CreateAsync(Post post)
    {
        _context.Posts.Add(post); 
        await _context.SaveChangesAsync(); 
        return post; 
    }
    public async Task UpdateAsync()
    {
        await _context.SaveChangesAsync(); 
    }
    public async Task RemoveAsync(Post post)
    {
        _context.Remove(post); 
        await _context.SaveChangesAsync(); 
    }

    public async Task<Post> GetByIdTrackingAsync(int id, int userId)
    {
        return await _context.Posts.Where(c => c.UserId == userId)
        .FirstOrDefaultAsync(p => p.Id == id); 
    }

    public PagedList<Post> GetAll(PaginationParameters parameters)
    {
        // return await _context.Posts.AsNoTracking() 
        //     .Skip((parameters.PageNumber - 1) * parameters.PageSize)
        //     .Take(parameters.PageSize)
        //     .ToListAsync(); 
        var posts = _context.Set<Post>().ToList();   
        return PagedList<Post>
            .ToPagedList(posts, parameters.PageNumber, parameters.PageSize); 
    }
}
