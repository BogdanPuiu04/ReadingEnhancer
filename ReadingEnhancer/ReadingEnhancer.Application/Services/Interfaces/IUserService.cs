using ReadingEnhancer.Domain.Entities;

namespace ReadingEnhancer.Application.Services.Interfaces;

public interface IUserService
{
    Task<User> Authenticate(string username, string password);
    Task<List<User>> GetAllAsync();
    Task<User> GetAsync(string id);
    Task<User> AddAsync(User user);
    Task<User> UpdateAsync(string id, User user);
    Task<bool> DeleteAsync(string id);
}