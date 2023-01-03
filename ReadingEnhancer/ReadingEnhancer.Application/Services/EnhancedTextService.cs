using MongoDB.Bson;
using ReadingEnhancer.Application.Models;
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

        private static bool ValidateUri(string url) => Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
                                                       uriResult.Scheme == Uri.UriSchemeHttps;

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
            return AppResponse<string>.Success("true");
        }

        public async Task<AppResponse<EnhancedText>> AddAsync(ReadingTextModel readingText)
        {
            var questions = (from question in readingText.Questions
                let answers =
                    question.Answers.Select(answer => new Answer()
                        {
                            Id = ObjectId.GenerateNewId().ToString(), Text = answer.Text, IsCorrect = answer.IsCorrect
                        })
                        .ToList()
                select new Question()
                    {Id = ObjectId.GenerateNewId().ToString(), Text = question.Text, Answers = answers}).ToList();

            var enhancedText = new EnhancedText()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Text = readingText.Text,
                QuestionsList = questions
            };
            var test = await _enhancedTextRepository.AddAsync(enhancedText);
            return AppResponse<EnhancedText>.Success(test);
        }

        public async Task<AppResponse<EnhancedText>> UpdateAsync(string id, EnhancedText text)
        {
            var response = await _enhancedTextRepository.UpdateOne(id, text);
            return AppResponse<EnhancedText>.Success(response);
        }

        public async Task<AppResponse<EnhancedText>> GetRandomTextAsync()
        {
            var enhancedText = await _enhancedTextRepository.GetRandomAsync();
            return AppResponse<EnhancedText>.Success(enhancedText);
        }

        public async Task<AppResponse<bool>> DeleteAsync(string id)
        {
            var result = await GetAsync(id);
            //  await _enhancedTextRepository.RemoveOne(result);
            return AppResponse<bool>.Success(true);
        }

        public async Task<AppResponse<string>> EnhanceText(string content)
        {
            if (ValidateUri(content)) return AppResponse<string>.Success("Text can't be a link");
            var body = "";
            try
            {
                var request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://bionic-reading1.p.rapidapi.com/convert"),
                    Headers =
                    {
                        {"X-RapidAPI-Key", "afb7bc5b7amsh840a44eaaf446ebp1d57f1jsn36b271e187bc"},
                        {"X-RapidAPI-Host", "bionic-reading1.p.rapidapi.com"},
                    },
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {
                            "content", content
                        },
                        {"response_type", "html"},
                        {"request_type", "html"},
                        {"fixation", "1"},
                        {"saccade", "10"},
                    }),
                };
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:" + e.Message);
            }

            return AppResponse<string>.Success(body);
        }

        public async Task<AppResponse<string>> EnhanceWebpage(string url)
        {
            if (!ValidateUri(url)) return AppResponse<string>.Success("Text needs to be a link");
            var res = "";
            try
            {
                var request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://bionic-reading1.p.rapidapi.com/convert"),
                    Headers =
                    {
                        {"X-RapidAPI-Key", "afb7bc5b7amsh840a44eaaf446ebp1d57f1jsn36b271e187bc"},
                        {"X-RapidAPI-Host", "bionic-reading1.p.rapidapi.com"},
                    },
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {
                            "content", url
                        },
                        {"response_type", "html"},
                        {"request_type", "html"},
                        {"fixation", "1"},
                        {"saccade", "10"},
                    }),
                };
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                res = await response.Content.ReadAsStringAsync();
                return AppResponse<string>.Success(res);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:" + e.Message);
            }

            return AppResponse<string>.Success(res);
        }
    }
}