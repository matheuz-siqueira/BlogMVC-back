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
    public PagedList<Post> GetAll(PaginationParameters parameters)
    {
        var posts = _context.Set<Post>().AsNoTracking().ToList();   
        return PagedList<Post>
            .ToPagedList(posts, parameters.PageNumber, parameters.PageSize); 
    }
    public async Task<List<Post>> GetAllOfUser(int userId)
    {
        return await _context.Posts.AsNoTracking()
            .Where(p => p.UserId == userId).ToListAsync(); 
     }
    public async Task<Post> GetByIdAsync(int id)
    {
        return  await _context.Posts.AsNoTracking().Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id); 
    }
    public async Task<Post> GetByIdTrackingAsync(int id, int userId)
    {
        return await _context.Posts.Where(c => c.UserId == userId)
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
}
