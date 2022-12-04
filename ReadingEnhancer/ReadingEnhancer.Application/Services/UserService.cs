using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using ReadingEnhancer.Application.Models;
using ReadingEnhancer.Application.Services.Interfaces;
using ReadingEnhancer.Common.Handlers;
using ReadingEnhancer.Domain.Entities;
using ReadingEnhancer.Domain.Repositories;

namespace ReadingEnhancer.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<ResponseUserModel> Authenticate(AuthUserModel authUserModel)
    {
        var matchingUser = await _userRepository.GetFirstAsync(user => user.Username == authUserModel.Username);
        if (!PasswordHelper.VerifyPassword(authUserModel.Password, matchingUser.Password))
            throw new Exception("Invalid Password");
        var res = new ResponseUserModel()
        {
            Name = $"{matchingUser.Name} {matchingUser.Surname}",
            Token = JwtHandler.GetJwtToken(matchingUser.Id, _configuration)
        };
        return res;
    }

    public Task<List<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseUserModel> AddAsync(RegisterUserModel registerUserModel)
    {
        var user = await _userRepository.GetFirstAsync(user => user.Username == registerUserModel.Username);
        var generatedUser = new User()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = registerUserModel.Name,
            Surname = registerUserModel.Surname,
            Username = registerUserModel.Username,
            IsAdmin = false,
            Password = PasswordHelper.ComputePasswordHash(registerUserModel.Password)
        };
        await _userRepository.AddAsync(generatedUser);
        return new ResponseUserModel()
        {
            Name = $"{generatedUser.Name} {generatedUser.Surname}",
            Token = JwtHandler.GetJwtToken(generatedUser.Id, _configuration)
        };
    }

    public Task<User> UpdateAsync(string id, User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }
}