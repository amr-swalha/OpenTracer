using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTracer
{
    public class TraceEvent
    {
        public string TraceName { get; set; }
        public string? TraceDescription { get; set; }
        protected float CpuUsage { get; set; } = 0;
        protected float MemoryUsage { get; set; } = 0;
        public TraceStatus Status { get; set; } = TraceStatus.Success;
        public string? ErrorMessage { get; set; }
        public string? StackTrace { get; set; }
        public DateTime EventDateTime { get; set; } = DateTime.UtcNow;
    }
}
