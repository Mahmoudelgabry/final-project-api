using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AiHttpClient : IAiHttpClient
    {
        private readonly HttpClient _httpClient;

        public AiHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChatResponseDto> SendChatAsync(
            ChatRequestDto request)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "ai-service/chat",
                request);

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<ChatResponseDto>();
        }
    }
}
