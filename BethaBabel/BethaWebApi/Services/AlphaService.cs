namespace BethaWebApi.Services
{
    #region

    using BethaWebApi.DTO;
    using System.Net;
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

            var stringContent = new StringContent(jsonContent);

            var response = await client.PostAsync($"api/alpha", stringContent);

            return response.StatusCode;
        }
    }
}
