namespace BethaWebApi.Controllers
{
    #region using

    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using DTO;
    using Betha.WebApi.HostedServices;

    #endregion

    [Route("api/[controller]")]
    [ApiController]
    public class BethaController : ControllerBase
    {
        private readonly ILogger<BethaController> _logger;

        private readonly IAlphaService _alphaService;
        private readonly ISomeWorkHostedService _someWorkHostedService;

        public BethaController(
            ILogger<BethaController> logger,
            IAlphaService alphaService,
            ISomeWorkHostedService someWorkHostedService)
        {
            _logger = logger;
            _alphaService = alphaService;
            _someWorkHostedService = someWorkHostedService;
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


        [HttpGet("StartHostedService")]
        public IActionResult StartHostedService()
        {
            string error_message = "The service it´s running.";
            string ok_message = "The service has been started.";

            if (_someWorkHostedService.IsRunning)
            {
                _logger.LogWarning(error_message);

                return BadRequest(error_message);
            }

            _someWorkHostedService.StartAsync(new CancellationToken());
            
            _logger.LogInformation(ok_message);

            return Ok(ok_message);
        }

        [HttpGet("StopHostedService")]
        public IActionResult StopHostedService()
        {
            string error_message = "The service it´s not running.";
            string ok_message = "The service has been stopped.";
            
            if (!_someWorkHostedService.IsRunning)
            {
                _logger.LogWarning(error_message);

                return BadRequest(error_message);
            }

            _someWorkHostedService.StopAsync(new CancellationToken());

            _logger.LogInformation(ok_message);

            return Ok(ok_message);
        }
    }
}
