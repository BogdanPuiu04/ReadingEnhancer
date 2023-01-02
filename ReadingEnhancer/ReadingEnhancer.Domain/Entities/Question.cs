namespace ReadingEnhancer.Domain.Entities;

public class Question : BaseEntity
{
    public string Text { get; set; }
    public List<Answer> Answers { get; set; }
}