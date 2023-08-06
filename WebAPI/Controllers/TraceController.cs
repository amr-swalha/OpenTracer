using OpenTracer.Core.Abstraction;
using OpenTracer.Core.Entities;

namespace WebAPI.Controllers
{
    public class TraceController : BaseController<Traces>
    {
        public TraceController(ITraceService traceService):base(traceService)
        {
                
        }
    }
}
