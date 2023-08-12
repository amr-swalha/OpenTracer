using Flurl.Http;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Text.Json;

namespace OpenTracerPackage
{
    public class OpenTracer : IOpenTracer
    {
        public static List<TraceEvent> Details { get; set; } = new();
        public void AddEvent(TraceEvent traceEvent)
        {
            Process p = System.Diagnostics.Process.GetCurrentProcess();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                var startTime = DateTime.UtcNow;
                var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
                System.Threading.Thread.Sleep(500);
                var endTime = DateTime.UtcNow;
                var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
                var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
                var totalMsPassed = (endTime - startTime).TotalMilliseconds;
                var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
                traceEvent.CpuUsage = (float)(cpuUsageTotal * 100);
                traceEvent.MemoryUsage = Environment.Is64BitProcess ? p.WorkingSet : p.WorkingSet64;
                traceEvent.MemoryUsage = traceEvent.MemoryUsage / (1024 * 1024);
            }
            Details.Add(traceEvent);
        }
        public async Task WriteEvents()
        {
            Console.WriteLine("Events written");
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration config = builder.Build();
            if (Details.Count > 0)
            {
                var response = await config["OpenTracer:API"].PostJsonAsync(new Traces { Details = JsonSerializer.Serialize(Details) });

            }
        }
    }
}
