namespace ReadingEnhancer.Application.Models;

public class AllUsersHighScores
{
    public List<UserHighScore> Users { get; set; }
}

public class UserHighScore
{
    public string Name { get; set; }
    public float HighScore { get; set; }
    public float ReadingSpeed { get; set; }
}