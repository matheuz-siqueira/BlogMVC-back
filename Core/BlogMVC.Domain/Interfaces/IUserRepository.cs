using BlogMVC.Domain.Entities;

namespace BlogMVC.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email); 
    Task CreateAccountAsync(User user); 
    void UpdatePasswordAsync(User user); 
    Task<User> GetByIdAsync(int id);
}
