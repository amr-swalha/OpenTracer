using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTracerPackage
{
    public interface IOpenTracer
    {
        void AddEvent(TraceEvent traceEvent);
        Task WriteEvents();
    }
}
