using AutoMapper;
using BlogMVC.Application.Dtos.User;
using BlogMVC.Application.Exceptions.BaseExceptions;
using BlogMVC.Application.Interfaces;
using BlogMVC.Application.Token;
using BlogMVC.Domain.Entities;
using BlogMVC.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogMVC.Application.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnityOfWork _unityOfWork;
    private readonly TokenService _tokenService;
    private readonly IUserLogged _userLogged;
    public UserService(IUserRepository repository, 
        IMapper mapper, IUnityOfWork unityOfWork, 
        TokenService tokenService, IUserLogged userLogged)
    {
        _repository = repository;
        _mapper = mapper; 
        _unityOfWork = unityOfWork; 
        _tokenService = tokenService; 
        _userLogged = userLogged; 
    }
    public async Task<TokenResponseJson> CreateAccount(CreateAccountRequestJson request)
    {
        var existing = await _repository.GetByEmailAsync(request.Email); 
        if(existing is not null)
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

    public async Task<GetProfileResponseJson> GetProfile()
    { 
        var user = await _userLogged.GetUser();
        var response = _mapper.Map<GetProfileResponseJson>(user);
        return response;
    }

    public async Task UpdatePassword(UpdatePasswordRequestJson request)
    {
        var logged = await _userLogged.GetUser();
        var user = await _repository.GetByIdAsync(logged.Id);
        if(!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Password))
        {
            throw new IncorretPasswordException(); 
        }
        user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        _repository.UpdatePasswordAsync(user);         
        await _unityOfWork.Commit();
    }
}
