namespace ReadingEnhancer.Domain.Entities
{
    public class EnhancedText : BaseEntity
    {
        public string Text { get; set; }
        public int Fixation { get; set; }
        public int Saccade { get; set; }
    }
}