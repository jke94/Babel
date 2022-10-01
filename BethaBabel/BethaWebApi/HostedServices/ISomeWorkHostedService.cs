namespace Betha.WebApi.HostedServices
{
    public interface ISomeWorkHostedService : IHostedService
    {
        public bool IsRunning { get; }
    }
}
