using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenTracer
{
    public class OpenTracer : IOpenTracer
    {
        public static List<TraceEvent> Details { get; set; } = new();
        public void AddEvent(TraceEvent traceEvent)
        {
            Process p = System.Diagnostics.Process.GetCurrentProcess();
            if (Environment.OSVersion.Platform== PlatformID.Win32NT )
            {
                var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                var ramCounter = new PerformanceCounter("Memory", "Available MBytes");
                traceEvent.CpuUsage = cpuCounter.NextValue();
                traceEvent.MemoryUsage = ramCounter.NextValue();
            }
           Details.Add(traceEvent);
        }
        public async void WriteEvents()
        {
            if (Details.Count > 0)
            {
                var response = await "Address".PostJsonAsync(new Traces { Details = JsonSerializer.Serialize(Details) });
               
            }
        }
    }
}
