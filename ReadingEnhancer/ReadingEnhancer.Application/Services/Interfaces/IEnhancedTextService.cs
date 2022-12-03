using ReadingEnhancer.Domain.Entities;

namespace ReadingEnhancer.Application.Services.Interfaces
{
    public interface IEnhancedTextService
    {
        Task<List<EnhancedText>> GetAllAsync();
        Task<string> GetAsync(string id);
        Task<EnhancedText> AddAsync(EnhancedText text);
        Task<EnhancedText> UpdateAsync(string id, EnhancedText text);
        Task<bool> DeleteAsync(string id);
    }
}