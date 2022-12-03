using ReadingEnhancer.Application.Models;
using ReadingEnhancer.Application.Services.Interfaces;
using ReadingEnhancer.Common.Handlers;
using ReadingEnhancer.Domain.Entities;
using ReadingEnhancer.Domain.Repositories;

namespace ReadingEnhancer.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResponseUserModel> Authenticate(AuthUserModel authUserModel)
    {
        var matchingUser = await _userRepository.GetFirstAsync(user => user.Name == authUserModel.Username);
        if (PasswordHelper.VerifyPassword(authUserModel.Password, matchingUser.Password))
            throw new Exception("Invalid Password");
        return null;
    }

    public Task<List<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<User> AddAsync(User user)
    {
        throw new NotImplementedException();
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