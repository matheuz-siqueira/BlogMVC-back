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
            .ToListAsync(); 
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
}
