namespace ReadingEnhancer.Domain.Entities;

public class Answer : BaseEntity
{
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
}