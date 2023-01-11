using ReadingEnhancer.Domain.Entities;

namespace ReadingEnhancer.Application.Models;

public class ReadingTextResponseModel
{
    public EnhancedText Text { get; set; }
    public int WordCount { get; set; }
}