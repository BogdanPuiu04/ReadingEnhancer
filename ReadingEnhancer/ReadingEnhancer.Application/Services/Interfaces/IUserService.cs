using ReadingEnhancer.Application.Models;
using ReadingEnhancer.Domain.Entities;

namespace ReadingEnhancer.Application.Services.Interfaces;

public interface IUserService
{
    Task<ResponseUserModel> Authenticate(AuthUserModel authUserModel);
    Task<List<User>> GetAllAsync();
    Task<User> GetAsync(string id);
    Task<User> AddAsync(User user);
    Task<User> UpdateAsync(string id, User user);
    Task<bool> DeleteAsync(string id);
}