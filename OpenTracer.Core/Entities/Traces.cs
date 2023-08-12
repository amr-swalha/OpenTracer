using OpenTracerPackage.Core.Abstraction;

namespace OpenTracerPackage.Core.Entities
{
    public class Traces : EntityRoot
    {
        public DateTime CreationDate { get; set; }
        public string Details { get; set; }
    }
}
