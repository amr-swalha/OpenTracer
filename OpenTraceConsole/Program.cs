// See https://aka.ms/new-console-template for more information
using OpenTracerPackage;

Console.WriteLine("Hello, World!");
IOpenTracer openTracer = new OpenTracer();
for (int i = 0; i < 10; i++)
{
    if (i % 2 == 0)
    {
        for (int j = 0; j < 10000; j++)
        {

        }
    } 
    openTracer.AddEvent(new TraceEvent
    {
        TraceName = $"trace {i}",
        TraceDescription = "Hello Open Trace",
        Status = TraceStatus.Success,
    });
}
await openTracer.WriteEvents();