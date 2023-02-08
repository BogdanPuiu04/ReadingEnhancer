using ReadingEnhancer.Application.Models;
using ReadingEnhancer.Common;

namespace ReadingEnhancer.Application.Services.Interfaces;

public interface IUserService
{
    Task<AppResponse<ResponseUserModel>> Authenticate(AuthUserModel authUserModel);
    Task<AppResponse<AllUsersResponseModel>> GetAllAsync(string userId);
    Task<AppResponse<ResponseUserModel>> AddAsync(RegisterUserModel user);
    Task<AppResponse<string>> RefreshToken(string id);
    Task<AppResponse<bool>> SubmitResult(ResultsRequestModel resultsRequestModel, string userId);
    Task<AppResponse<AllUsersHighScores>> GetAllUsersHighScore();
    Task<AppResponse<bool>> ChangeAdmin(string userId, string thisUser);
}