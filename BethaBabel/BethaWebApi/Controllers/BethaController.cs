namespace BethaWebApi.Controllers
{
    #region using

    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using DTO;

    #endregion

    public class BethaController : Controller
    {
        private readonly ILogger<BethaController> _logger;

        private readonly IAlphaService _alphaService;

        public BethaController(
            ILogger<BethaController> logger,
            IAlphaService alphaService)
        {
            _logger = logger;
            _alphaService = alphaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(BethaDto bethaDto)
        {

            var message = $"Command: {bethaDto.Command}, Argument: {bethaDto.Argument}.";

            var tastResult = await _alphaService.PostAsync(bethaDto);

            if (tastResult != HttpStatusCode.OK)
            {
                return BadRequest(tastResult.ToString());
            }

            return Ok(tastResult.ToString());
        }
    }
}
