using ReadingEnhancer.Domain.Entities;

namespace ReadingEnhancer.Application.Models;

public class AllReadingTextsResponse
{
    public List<EnhancedText> Texts { get; set; }
}