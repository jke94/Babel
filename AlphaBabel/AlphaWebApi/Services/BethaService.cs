using AlphaWebApi.DTO;
using System.Net;
using System.Text.Json;

namespace AlphaWebApi.Services
{
    public class BethaService : IBethaService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public BethaService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<HttpStatusCode> PostAsync(AlphaDto alphaDto)
        {
            var client = httpClientFactory.CreateClient("bethaService");

            var jsonContent = JsonSerializer.Serialize(alphaDto);

            var stringContent = new StringContent(jsonContent);

            var response = await client.PostAsync($"api/betha", stringContent);

            return response.StatusCode;
        }
    }
}
