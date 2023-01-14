namespace ReadingEnhancer.Application.Models;

public class ResponseUserModel
{
    public string Name { get; set; }
    public string Token { get; set; }
    public bool IsAdmin { get; set; }
    public float HighScore { get; set; }
    public float ReadingSpeed { get; set; }
}