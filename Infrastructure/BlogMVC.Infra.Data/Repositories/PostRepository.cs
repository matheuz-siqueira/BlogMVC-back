using BlogMVC.Domain.Entities;
using BlogMVC.Domain.Interfaces;
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
    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _context.Posts.AsNoTracking()
            .Include(p => p.Tags)
            .ToListAsync(); 
    }
    public async Task<Post> GetByIdAsync(int id, bool tracking = true)
    {
        return (tracking)
            ? await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == id) 
            : await _context.Posts.AsNoTracking()
                .Include(p => p.Comments)
                .Include(p => p.Tags)
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
