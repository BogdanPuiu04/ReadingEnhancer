using ReadingEnhancer.Application.Services.Interfaces;
using ReadingEnhancer.Common;
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

        public async Task<AppResponse<List<EnhancedText>>> GetAllAsync()
        {
            var enhancedTexts = await _enhancedTextRepository.GetAllAsync();
            return AppResponse<List<EnhancedText>>.Success(enhancedTexts);
        }

        public async Task<AppResponse<string>> GetAsync(string id)
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
            return AppResponse<string>.Success(body);
        }

        public async Task<AppResponse<EnhancedText>> AddAsync(EnhancedText text)
        {
            var enhancedText = await _enhancedTextRepository.AddAsync(text);
            return AppResponse<EnhancedText>.Success(enhancedText);
        }

        public async Task<AppResponse<EnhancedText>> UpdateAsync(string id, EnhancedText text)
        {
            var response = await _enhancedTextRepository.UpdateOne(id, text);
            return AppResponse<EnhancedText>.Success(response);
        }

        public async Task<AppResponse<bool>> DeleteAsync(string id)
        {
            var result = await GetAsync(id);
            //  await _enhancedTextRepository.RemoveOne(result);
            return AppResponse<bool>.Success(true);
        }
    }
}