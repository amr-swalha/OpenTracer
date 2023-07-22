using System.Diagnostics;
using System.Text.Json;
using WebApp.Models;

namespace WebApp.Services
{
    public class TraceService
    {
        private readonly AppDataAccess _appDataAccess;

        public TraceService(AppDataAccess appDataAccess)
        {
            _appDataAccess = appDataAccess;
        }
        public async Task Add(TraceEvent traceEvent)
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
            var ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            traceEvent.CpuUsage = cpuCounter.NextValue();
            traceEvent.MemoryUsage = ramCounter.NextValue();
            await _appDataAccess.AddAsync(new Traces { Details = JsonSerializer.Serialize(traceEvent), CreationDate = DateTime.UtcNow });
            await _appDataAccess.SaveChangesAsync();
        }
    }
}
