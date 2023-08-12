﻿using OpenTracerPackage.Core.Abstraction;
using OpenTracerPackage.Core.Entities;

namespace WebAPI.Controllers
{
    public class TraceController : BaseController<Traces>
    {
        public TraceController(ITraceService traceService):base(traceService)
        {
                
        }
    }
}
