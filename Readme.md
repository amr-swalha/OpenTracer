## OpenTracer 
### A small tool to help you collect data from your application

The aim is to help you diagnostic your application and see the usage of the RAM and CPU on certain events.

You can download the package from NuGet using this URL:

https://www.nuget.org/packages/OpenTracer/

Sample Code

    IOpenTracer openTracer = new OpenTracer("https://localhost:32770/api/Trace");
    for (int i = 0; i < 10; i++)
    { 
        openTracer.AddEvent(new TraceEvent
        {
            TraceName = $"trace {i}",
            TraceDescription = "Hello Open Trace",
            Status = TraceStatus.Success,
        });
    }
    await openTracer.WriteEvents();

As you can see, you just need to point the library toward the API endpoint and the library will handle the writing of the events.

And you just need to call the WriteEvents method and it will write all the events for you.

### ASP.NET Core + IHostedService

To add an easy way of logging to your ASP.NET Core applicaiton, you can simply inject the package wtih a service and provide the endpoint as follow:

    builder.Services.AddScoped<IOpenTracer>(sp => new OpenTracer("https://localhost:7210/api/Trace"));

And then just you have to inject the code to log the event. You can use a hosted service and call it every 5 minutes to send out the events to the API endpoints.

The following is a sample code that's send all the events every 20 seconds:

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
                    var openTracerService = scope.ServiceProvider.GetRequiredService<IOpenTracer>();
                    await openTracerService.WriteEvents();
                }
            }
        }
    }

make sure to reference it inside the services:

            builder.Services.AddHostedService<EventBackgroundService>();