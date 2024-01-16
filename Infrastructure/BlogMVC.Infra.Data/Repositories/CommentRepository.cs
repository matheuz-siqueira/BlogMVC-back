using System.Reflection.Metadata.Ecma335;
using BlogMVC.Domain.Entities;
using BlogMVC.Domain.Interfaces;
using BlogMVC.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogMVC.Infra.Data.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context; 
    public CommentRepository(AppDbContext context)
    {
        _context = context; 
    }

    public async Task<IEnumerable<Comment>> GetAllAsync()
    {
        return await _context.Comments.AsNoTracking().ToListAsync(); 
    }
    public async Task<Comment> GetByIdAsync(int id, bool tracking = true)
    {
        return (tracking)    
            ?  await _context.Comments.FirstOrDefaultAsync(c => c.Id == id) 
            :  await _context.Comments.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);  
    }
    public async Task<Comment> CreateAsync(Comment comment)
    {
        _context.Comments.Add(comment); 
        await _context.SaveChangesAsync();
        return comment; 
    }   
    public async Task UpdateAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task RemoveAsync(Comment comment)
    {
        _context.Remove(comment); 
        await _context.SaveChangesAsync(); 
    }
}
