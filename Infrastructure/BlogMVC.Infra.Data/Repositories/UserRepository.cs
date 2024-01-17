using BlogMVC.Domain.Entities;
using BlogMVC.Domain.Interfaces;
using BlogMVC.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogMVC.Infra.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context; 
    } 
    public async Task CreateAccountAsync(User user)
    {
        _context.Users.Add(user); 
        await _context.SaveChangesAsync(); 
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(c => c.Email.Equals(email));  
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
    }

    public void UpdatePasswordAsync(User user)
    {
        _context.Users.Update(user);        
    }
}
