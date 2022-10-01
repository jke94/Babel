namespace Betha.WebApi.HostedServices
{
    public class SomeWorkHostedService : BackgroundService, ISomeWorkHostedService
    {
        #region Fields
        
        private readonly int _wait = 1000;

        private readonly ILogger<SomeWorkHostedService> _logger;

        private int _count = 0;

        private bool _isRunning = false;

        #endregion

        #region Cosntructor

        public SomeWorkHostedService(
            ILogger<SomeWorkHostedService> logger)
        {
            _logger = logger;
        }

        #endregion

        public bool IsRunning => _isRunning;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _isRunning = true;
            string message = string.Empty;

            do
            {
                message = $"[{_count}] Method: SomeWorkHostedService.ExecuteAsync()";
                _logger.LogInformation(message);

                _count++;

                await Task.Delay(_wait);
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _isRunning = false;
            await base.StopAsync(stoppingToken);
        }
    }
}
