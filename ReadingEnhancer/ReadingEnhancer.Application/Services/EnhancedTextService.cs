using ReadingEnhancer.Domain.Entities;
using ReadingEnhancer.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingEnhancer.Application.Services
{
    public class EnhancedTextService : IEnhancedTextService
    {
        private readonly IEnhancedTextRepository _enhancedTextRepository;

        public EnhancedTextService(IEnhancedTextRepository enhancedTextRepository)
        {
            _enhancedTextRepository = enhancedTextRepository;
        }

        public async Task<List<EnhancedText>> GetAllAsync()
        {
            var enhancedTexts = await _enhancedTextRepository.GetAllAsync();
            return enhancedTexts;
        }

        public async Task<EnhancedText> GetAsync(string id)
        {
            var enhancedText = await _enhancedTextRepository.GetFirstAsync(id);
            return enhancedText;
        }

        public async Task<EnhancedText> AddAsync(EnhancedText text)
        {
            var enhancedText = await _enhancedTextRepository.AddAsync(text);
            return enhancedText;
        }

        public async Task<EnhancedText> UpdateAsync(string id, EnhancedText text)
        {
            var response = await _enhancedTextRepository.UpdateOne(id, text);
            return response;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await GetAsync(id);
            await _enhancedTextRepository.RemoveOne(result);
            return true;
        }
    }
}