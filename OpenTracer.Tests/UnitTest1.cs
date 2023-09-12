namespace OpenTracerPackage.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            IOpenTracer openTracer = new OpenTracer("");
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
        }
    }
}