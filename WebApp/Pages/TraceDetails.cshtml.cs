using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebApp.Models;

namespace WebApp.Pages
{
    public class TraceDetailsModel : PageModel
    {
        [FromQuery]
        public string Id { get; set; }
        public Traces Trace { get; set; }
        public TraceMap TraceMap { get; set; } = new();
        private readonly AppDataAccess _appDataAccess;
        public TraceDetailsModel(AppDataAccess appDataAccess)
        {
            _appDataAccess = appDataAccess;
        }
        public void OnGet()
        {
            var itemId = new Guid(Id);
            Trace = _appDataAccess.Set<Traces>().Where(y => y.Id == itemId).FirstOrDefault();
            TraceMap.Id = Trace.Id;
            TraceMap.CreationDate = Trace.CreationDate;
            TraceMap.Details = JsonSerializer.Deserialize<List<TraceEvent>>(Trace.Details);
        }
    }
}
