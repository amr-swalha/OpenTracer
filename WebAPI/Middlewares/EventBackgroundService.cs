using OpenTracerPackage;

namespace WebAPI.Middlewares
{
    public class EventBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public EventBackgroundService(IServiceScopeFactory serviceScopeFactory) => _serviceScopeFactory = serviceScopeFactory;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(20));
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    //var openTracerService = scope.ServiceProvider.GetRequiredService<IOpenTracer>();
                    //await openTracerService.WriteEvents();
                }
            }
        }
    }
}
