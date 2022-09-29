namespace AlphaWebApi.Services
{
    #region

    using AlphaWebApi.DTO;
    using System.Net;

    #endregion

    public interface IBethaService
    {
        Task<HttpStatusCode> PostAsync(AlphaDto alphaDto);
    }
}
