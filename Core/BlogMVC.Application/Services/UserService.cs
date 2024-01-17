using AutoMapper;
using BlogMVC.Application.Dtos.User;
using BlogMVC.Application.Exceptions.BaseExceptions;
using BlogMVC.Application.Interfaces;
using BlogMVC.Application.Token;
using BlogMVC.Domain.Entities;
using BlogMVC.Domain.Interfaces;

namespace BlogMVC.Application.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnityOfWork _unityOfWork;
    private readonly TokenService _tokenService;
    public UserService(IUserRepository repository, 
        IMapper mapper, IUnityOfWork unityOfWork, TokenService tokenService)
    {
        _repository = repository;
        _mapper = mapper; 
        _unityOfWork = unityOfWork; 
        _tokenService = tokenService; 
    }
    public async Task<TokenResponseJson> CreateAccount(CreateAccountRequestJson request)
    {
        var existing = await _repository.GetEmailAsync(request.Email); 
        if(existing)
        {
            throw new BlogException("Usuáiro já existe"); 
        }
        var user = _mapper.Map<User>(request); 
        user.CreatedAt = DateTime.Now; 
        user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password); 
        await _repository.CreateAccountAsync(user); 
        await _unityOfWork.Commit();
        var token = _tokenService.GenerateToken(user.Email); 
        var resposne = new TokenResponseJson
        {
            Email = user.Email, 
            Token = token 
        }; 
        return resposne;
    }
}
