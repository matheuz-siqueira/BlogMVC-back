using BlogMVC.Domain.Entities;

namespace BlogMVC.Domain.Interfaces;

public interface IUserRepository
{
    Task<bool> GetEmailAsync(string email); 
    Task CreateAccountAsync(User user); 
}
