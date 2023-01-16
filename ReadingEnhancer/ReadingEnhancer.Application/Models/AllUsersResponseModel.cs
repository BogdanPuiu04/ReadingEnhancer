namespace ReadingEnhancer.Application.Models;

public class AllUsersResponseModel
{
    public List<UserResponseModel> AllUsers;
}

public class UserResponseModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public bool IsAdmin { get; set; }
}