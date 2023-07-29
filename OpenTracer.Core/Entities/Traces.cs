using OpenTracer.Core.Abstraction;

namespace OpenTracer.Core.Entities
{
    public class Traces : EntityRoot
    {
        public DateTime CreationDate { get; set; }
        public string Details { get; set; }
    }
}
