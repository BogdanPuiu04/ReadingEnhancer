namespace ReadingEnhancer.Common.CustomExceptions;

public static class ValidationMessages
{
    public const string InternalError = "Something went wrong.";
    public const string SurnameLength = "Surname length should be at least 2.";
    public const string UsernameLength = "Username length should at least 3 characters long.";
    public const string PasswordLength = "Password length should be at least at least 8 characters long.";
    public const string ConflictError = "There is already an entity with these credentials.";
}