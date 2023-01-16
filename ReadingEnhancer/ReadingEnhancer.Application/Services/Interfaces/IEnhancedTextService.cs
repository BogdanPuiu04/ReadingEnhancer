using ReadingEnhancer.Application.Models;
using ReadingEnhancer.Common;
using ReadingEnhancer.Domain.Entities;

namespace ReadingEnhancer.Application.Services.Interfaces
{
    public interface IEnhancedTextService
    {
        Task<AppResponse<AllReadingTextsResponse>> GetAllAsync(string user);
        Task<AppResponse<ReadingTextResponseModel>> GetRandomTextAsync();
        Task<AppResponse<EnhancedText>> GetAsync(string id);
        Task<AppResponse<EnhancedText>> AddAsync(ReadingTextModel text,string userId);
        Task<AppResponse<EnhancedText>> UpdateAsync(string id, EnhancedText text,string userId);
        Task<AppResponse<bool>> DeleteAsync(string id,string userId);
        Task<AppResponse<string>> EnhanceText(string content);
        Task<AppResponse<string>> EnhanceWebpage(string url);
        Task<AppResponse<EnhancedText>> AddNewText(EnhancedText text, string userId);
    }
}