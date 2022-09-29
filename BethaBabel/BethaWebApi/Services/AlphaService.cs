namespace BethaWebApi.Services
{
    #region

    using BethaWebApi.DTO;
    using System.Net;
    using System.Text;
    using System.Text.Json;

    #endregion

    public class AlphaService : IAlphaService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AlphaService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpStatusCode> PostAsync(BethaDto bethaDto)
        {
            var client = _httpClientFactory.CreateClient("alphaService");

            var jsonContent = JsonSerializer.Serialize(bethaDto);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"api/alpha/ReceiveMessage", stringContent);

            return response.StatusCode;
        }
    }
}
