namespace ReadingEnhancer.Application.Models;

public class ReadingTextModel
{
    public string Text { get; set; }
    public List<QuestionRequestModel> Questions { get; set; }
}

public class QuestionRequestModel
{
    public string Text { get; set; }
    public List<AnswerRequestModel> Answers { get; set; }
}

public class AnswerRequestModel
{
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
}