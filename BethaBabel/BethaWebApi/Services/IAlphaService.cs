namespace BethaWebApi.Services
{
    #region

    using BethaWebApi.DTO;
    using System.Net;

    #endregion

    public interface IAlphaService
    {
        Task<HttpStatusCode> PostAsync(BethaDto bethaDto);
    }
}
