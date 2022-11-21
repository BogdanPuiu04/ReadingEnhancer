using ReadingEnhancer.Application.Services.Interfaces;
using ReadingEnhancer.Domain.Entities;

namespace ReadingEnhancer.Application.Services;

public class UserService : IUserService
{
    
    public Task<User> Authenticate(string username, string password)
    {
        throw new NotImplementedException();
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