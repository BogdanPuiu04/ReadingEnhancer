using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using ReadingEnhancer.Application.Models;
using ReadingEnhancer.Application.Services.Interfaces;
using ReadingEnhancer.Common;
using ReadingEnhancer.Common.CustomExceptions;
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
            Token = JwtHandler.GetJwtToken(matchingUser.Id, _configuration),
            IsAdmin = matchingUser.IsAdmin,
            HighScore = matchingUser.HighScore,
            ReadingSpeed = matchingUser.ReadingSpeed
        };
        return AppResponse<ResponseUserModel>.Success(res);
    }

    public async Task<AppResponse<AllUsersResponseModel>> GetAllAsync(string userId)
    {
        IsAdmin(userId);
        var users = await _userRepository.GetAllAsync();
        var allUsers = new AllUsersResponseModel()
        {
            AllUsers = new List<UserResponseModel>()
        };
        foreach (var userResponse in users.Select(user => new UserResponseModel()
                 {
                     Id = user.Id,
                     IsAdmin = user.IsAdmin,
                     LastName = user.LastName,
                     Name = user.Name,
                     Username = user.Username
                 }))
        {
            allUsers.AllUsers.Add(userResponse);
        }

        return AppResponse<AllUsersResponseModel>.Success(allUsers);
    }

    public Task<AppResponse<User>> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<AppResponse<ResponseUserModel>> AddAsync(RegisterUserModel registerUserModel)
    {
        //check if user is not null
        var user = await _userRepository.GetFirstAsync(user => user.Username == registerUserModel.Username);

        var generatedUser = new User()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = registerUserModel.FirstName,
            LastName = registerUserModel.LastName,
            Username = registerUserModel.Username,
            IsAdmin = false,
            Password = PasswordHelper.ComputePasswordHash(registerUserModel.Password),
            HighScore = 0,
            ReadingSpeed = 0
        };
        await _userRepository.AddAsync(generatedUser);
        return AppResponse<ResponseUserModel>.Success((new ResponseUserModel
        {
            Name = $"{generatedUser.Name} {generatedUser.LastName}",
            Token = JwtHandler.GetJwtToken(generatedUser.Id, _configuration),
            IsAdmin = generatedUser.IsAdmin,
            HighScore = generatedUser.HighScore,
            ReadingSpeed = generatedUser.ReadingSpeed
        }));
    }

    public async Task<AppResponse<User>> UpdateAsync(string id, User user)
    {
        var newUser = await _userRepository.UpdateOne(id, user);
        return AppResponse<User>.Success(newUser);
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

    public async Task<AppResponse<bool>> SubmitResult(ResultsRequestModel result, string userId)
    {
        var user = await _userRepository.GetFirstAsync(userId);
        user.HighScore = result.HighScore;
        user.ReadingSpeed = result.ReadingSpeed;
        await UpdateAsync(userId, user);
        return AppResponse<bool>.Success(true);
    }

    public async Task<AppResponse<AllUsersHighScores>> GetAllUsersHighScore()
    {
        var users = await _userRepository.GetAllAsync();
        var filteredUsers = users.OrderByDescending(user => user.HighScore).ThenBy(user => user.ReadingSpeed).ToList();
        var response = new AllUsersHighScores()
        {
            Users = new List<UserHighScore>()
        };
        var count = 0;
        foreach (var user in filteredUsers)
        {
            if (count < 3)
            {
                response.Users.Add(new UserHighScore
                {
                    HighScore = user.HighScore,
                    Name = $"{user.Name} {user.LastName}",
                    ReadingSpeed = user.ReadingSpeed
                });
                count++;
            }
            else break;
        }

        return AppResponse<AllUsersHighScores>.Success(response);
    }

    public async Task<AppResponse<bool>> ChangeAdmin(string id, string thisUser)
    {
        IsAdmin(thisUser);
        var user = await _userRepository.GetFirstAsync(id);
        if (user.Username.Equals("bpuiu"))
            throw new UnauthorizedException("Can't remove the admin rights of owner!");
        user.IsAdmin = !user.IsAdmin;
        await _userRepository.UpdateOne(id, user);
        return AppResponse<bool>.Success(true);
    }

    private async void IsAdmin(string userId)
    {
        var user = await _userRepository.GetFirstAsync(userId);
        if (!user.IsAdmin)
            throw new UnauthorizedException("User is not authorized");
    }
}