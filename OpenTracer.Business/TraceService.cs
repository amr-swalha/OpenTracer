﻿using OpenTracer.Core.Abstraction;
using OpenTracer.Core.Entities;

namespace OpenTracer.Business
{
    public class TraceService : RootService<Traces>, ITraceService
    {
        private readonly IRepository<Traces> _reposity;
        public TraceService(IRepository<Traces> reposity):base(reposity)
        {
            
        }
    }
}
