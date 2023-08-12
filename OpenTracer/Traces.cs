using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTracerPackage
{
    internal class Traces
    {
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public string Details { get; set; }
    }
}
