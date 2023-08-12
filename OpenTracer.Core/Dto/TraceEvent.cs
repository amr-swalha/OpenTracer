namespace OpenTracerPackage.Core.Dto
{
    public class TraceEvent
    {
        public string TraceName { get; set; }
        public string? TraceDescription { get; set; }
        public float CpuUsage { get; set; } = 0;
        public float MemoryUsage { get; set; } = 0;
        public int Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? StackTrace { get; set; }
        public DateTime EventDateTime { get; set; } = DateTime.UtcNow;
    }
}
