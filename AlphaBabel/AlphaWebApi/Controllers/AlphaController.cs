namespace AlphaWebApi.Controllers
{
    #region using

    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using AlphaWebApi.Services;
    using DTO;

    #endregion

    [Route("api/[controller]")]
    [ApiController]
    public class AlphaController : ControllerBase
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

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage(AlphaDto alphaDto)
        {
            var tastResult = await _bethaService.PostAsync(alphaDto);

            if (tastResult != HttpStatusCode.OK)
            {
                return BadRequest(tastResult.ToString());
            }

            return Ok(tastResult.ToString());
        }

        [HttpPost("ReceiveMessage")]
        public IActionResult ReceiveMessage(AlphaDto alphaDto)
        {
            if(alphaDto is null)
            {
                return BadRequest(alphaDto);
            }

            var message = $"Command: {alphaDto.Command}, Argument: {alphaDto.Argument}.";

            _logger.LogInformation(message);

            return Ok(alphaDto);
        }
    }
}
