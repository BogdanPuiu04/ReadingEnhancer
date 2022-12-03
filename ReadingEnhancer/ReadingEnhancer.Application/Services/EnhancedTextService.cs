using ReadingEnhancer.Application.Services.Interfaces;
using ReadingEnhancer.Domain.Entities;
using ReadingEnhancer.Domain.Repositories;

namespace ReadingEnhancer.Application.Services
{
    public class EnhancedTextService : IEnhancedTextService
    {
        private readonly IEnhancedTextRepository _enhancedTextRepository;
        private readonly HttpClient _httpClient;

        public EnhancedTextService(IEnhancedTextRepository enhancedTextRepository, HttpClient httpClient)
        {
            _enhancedTextRepository = enhancedTextRepository;
            _httpClient = httpClient;
        }

        public async Task<List<EnhancedText>> GetAllAsync()
        {
            var enhancedTexts = await _enhancedTextRepository.GetAllAsync();
            return enhancedTexts;
        }

        public async Task<string> GetAsync(string id)
        {
            var enhancedText = await _enhancedTextRepository.GetFirstAsync(id);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Headers =
                {
                    {"X-RapidAPI-Key", "afb7bc5b7amsh840a44eaaf446ebp1d57f1jsn36b271e187bc"},
                    {"X-RapidAPI-Host", "bionic-reading1.p.rapidapi.com"},
                },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {
                        "content", enhancedText.Text
                    },
                    {"response_type", "html"},
                    {"request_type", "html"},
                    {"fixation", enhancedText.Fixation.ToString()},
                    {"saccade", enhancedText.Saccade.ToString()},
                }),
            };
            var responseString = await _httpClient.SendAsync(request);
            responseString.EnsureSuccessStatusCode();
            var body = await responseString.Content.ReadAsStringAsync();
            return body;
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
          //  await _enhancedTextRepository.RemoveOne(result);
            return true;
        }
    }
}