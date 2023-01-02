using ReadingEnhancer.Application.Models;
using ReadingEnhancer.Common;
using ReadingEnhancer.Domain.Entities;

namespace ReadingEnhancer.Application.Services.Interfaces
{
    public interface IEnhancedTextService
    {
        Task<AppResponse<List<EnhancedText>>> GetAllAsync();
        Task<AppResponse<string>> GetAsync(string id);
        Task<AppResponse<EnhancedText>> AddAsync(ReadingTextModel text);
        Task<AppResponse<EnhancedText>> UpdateAsync(string id, EnhancedText text);
        Task<AppResponse<bool>> DeleteAsync(string id);
        Task<AppResponse<string>> EnhanceText(string content);
        Task<AppResponse<string>> EnhanceWebpage(string url);
    }
}