using ReadingEnhancer.Application.Models;
using ReadingEnhancer.Common;
using ReadingEnhancer.Domain.Entities;

namespace ReadingEnhancer.Application.Services.Interfaces;

public interface IUserService
{
    Task<AppResponse<ResponseUserModel>> Authenticate(AuthUserModel authUserModel);
    Task<AppResponse<List<User>>> GetAllAsync();
    Task<AppResponse<User>> GetAsync(string id);
    Task<AppResponse<ResponseUserModel>> AddAsync(RegisterUserModel user);
    Task<AppResponse<User>> UpdateAsync(string id, User user);
    Task<AppResponse<bool>> DeleteAsync(string id);
    Task<AppResponse<string>> RefreshToken(string Id);
    Task<AppResponse<bool>> SubmitResult(ResultsRequestModel resultsRequestModel,string userId);
}