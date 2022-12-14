namespace AlphaWebApi.Services
{
    #region

    using AlphaWebApi.DTO;
    using System.Net;
    using System.Text;
    using System.Text.Json;

    #endregion

    public class BethaService : IBethaService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BethaService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpStatusCode> PostAsync(AlphaDto alphaDto)
        {
            var client = _httpClientFactory.CreateClient("bethaService");

            var jsonContent = JsonSerializer.Serialize(alphaDto);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"api/betha/ReceiveMessage", stringContent);

            return response.StatusCode;
        }
    }
}
