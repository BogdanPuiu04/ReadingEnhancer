namespace ReadingEnhancer.Domain.Entities
{
    public class EnhancedText : BaseEntity
    {
        public string Text { get; set; }
        public List<Question> QuestionsList { get; set; }
    }
}