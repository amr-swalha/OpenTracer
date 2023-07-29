namespace OpenTracer.Core.Dto
{
    public class TraceMap
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public List<TraceEvent> Details { get; set; } = new();
    }
}
