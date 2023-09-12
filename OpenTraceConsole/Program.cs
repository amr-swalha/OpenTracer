// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using OpenTracerPackage;
using System.Configuration;

Console.WriteLine("Hello, World!");
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