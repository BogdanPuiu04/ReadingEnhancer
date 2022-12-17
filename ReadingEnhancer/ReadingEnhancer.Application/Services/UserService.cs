using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using ReadingEnhancer.Application.Models;
using ReadingEnhancer.Application.Services.Interfaces;
using ReadingEnhancer.Common;
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

    public async Task<AppResponse<ResponseUserModel>> Authenticate(AuthUserModel authUserModel)
    {
        var matchingUser = await _userRepository.GetFirstAsync(user => user.Username == authUserModel.Username);
        if (!PasswordHelper.VerifyPassword(authUserModel.Password, matchingUser.Password))
            throw new Exception("Invalid Password");
        var res = new ResponseUserModel()
        {
            Name = $"{matchingUser.Name} {matchingUser.LastName}",
            Token = JwtHandler.GetJwtToken(matchingUser.Id, _configuration)
        };
        return AppResponse<ResponseUserModel>.Success(res);
    }

    public Task<AppResponse<List<User>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<AppResponse<User>> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<AppResponse<ResponseUserModel>> AddAsync(RegisterUserModel registerUserModel)
    {
        var user = await _userRepository.GetFirstAsync(user => user.Username == registerUserModel.Username);
        var generatedUser = new User()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = registerUserModel.FirstName,
            LastName = registerUserModel.LastName,
            Username = registerUserModel.Username,
            IsAdmin = false,
            Password = PasswordHelper.ComputePasswordHash(registerUserModel.Password)
        };
        await _userRepository.AddAsync(generatedUser);
        return AppResponse<ResponseUserModel>.Success((new ResponseUserModel
        {
            Name = $"{generatedUser.Name} {generatedUser.LastName}",
            Token = JwtHandler.GetJwtToken(generatedUser.Id, _configuration)
        }));
    }

    public Task<AppResponse<User>> UpdateAsync(string id, User user)
    {
        throw new NotImplementedException();
    }

    public Task<AppResponse<bool>> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<AppResponse<string>> RefreshToken(string id)
    {
        var refreshToken = JwtHandler.GetJwtToken(id, _configuration);
        var refreshTokenResponse = new RefreshTokenResponseModel
        {
            Token = refreshToken
        };
        return await Task.FromResult(AppResponse<string>.Success(refreshTokenResponse.Token));
    }
}