using OpenTracerPackage.Core.Abstraction;
using OpenTracerPackage.Core.Entities;

namespace OpenTracerPackage.Business
{
    public class TraceService : RootService<Traces>, ITraceService
    {
        private readonly IRepository<Traces> _reposity;
        public TraceService(IRepository<Traces> reposity):base(reposity)
        {
            
        }
    }
}
