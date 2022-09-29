namespace BethaWebApi.Controllers
{
    #region using

    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using DTO;

    #endregion

    [Route("api/[controller]")]
    [ApiController]
    public class BethaController : ControllerBase
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

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage(BethaDto bethaDto)
        {
            var tastResult = await _alphaService.PostAsync(bethaDto);

            if (tastResult != HttpStatusCode.OK)
            {
                return BadRequest(tastResult.ToString());
            }

            return Ok(tastResult.ToString());
        }

        [HttpPost("ReceiveMessage")]
        public IActionResult ReceiveMessage(BethaDto alphaDto)
        {
            if (alphaDto is null)
            {
                return BadRequest(alphaDto);
            }

            var message = $"Command: {alphaDto.Command}, Argument: {alphaDto.Argument}.";

            _logger.LogInformation(message);

            return Ok(alphaDto);
        }
    }
}
