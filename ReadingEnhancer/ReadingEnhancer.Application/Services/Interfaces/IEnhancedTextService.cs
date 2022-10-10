using ReadingEnhancer.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ReadingEnhancer.Application.Services
{
    public interface IEnhancedTextService
    {
        Task<List<EnhancedText>> GetAllAsync();
        Task<EnhancedText> GetAsync(string id);
        Task<EnhancedText> AddAsync(EnhancedText text);
        Task<EnhancedText> UpdateAsync(string id, EnhancedText text);
        Task<bool> DeleteAsync(string id);
    }
}