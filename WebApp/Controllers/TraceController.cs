using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TraceController : ControllerBase
{
    private readonly AppDataAccess _appDataAccess;
    private readonly TraceService traceService;

    public TraceController(AppDataAccess appDataAccess,TraceService traceService)
    {
        _appDataAccess = appDataAccess;
        this.traceService = traceService;
    }
    [HttpPost("[action]")]
    public async Task<List<Traces>> Post(List<Traces> traces)
    {
        await traceService.Add(new TraceEvent
        {
            TraceName = "Beginning",
            Status = (int)TraceStatus.Success,
        });
        await _appDataAccess.AddRangeAsync(traces);
        await _appDataAccess.SaveChangesAsync();
        await traceService.Add(new TraceEvent
        {
            TraceName = "End",
            Status = (int)TraceStatus.Success,
        });
        return traces;
    }
}