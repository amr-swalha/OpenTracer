using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTracer
{
    public interface IOpenTracer
    {
        void AddEvent(TraceEvent traceEvent);
    }
}
