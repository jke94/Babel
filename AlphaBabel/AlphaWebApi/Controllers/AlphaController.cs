using Microsoft.AspNetCore.Mvc;

namespace AlphaWebApi.Controllers
{
    using AlphaWebApi.Services;
    #region using

    using DTO;
    using System.Net;

    #endregion

    public class AlphaController : Controller
    {
        private readonly ILogger<AlphaController> _logger;

        private readonly IBethaService _bethaService;

        public AlphaController(
            ILogger<AlphaController> logger,
            IBethaService bethaService)
        {
            _logger = logger;
            _bethaService = bethaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(AlphaDto alphaDto)
        {

            var message = $"Command: {alphaDto.Command}, Argument: {alphaDto.Argument}.";

            var tastResult = await _bethaService.PostAsync(alphaDto);

            if (tastResult != HttpStatusCode.OK)
            {
                return BadRequest(tastResult.ToString());
            }

            return Ok(tastResult.ToString());
        }
    }
}
