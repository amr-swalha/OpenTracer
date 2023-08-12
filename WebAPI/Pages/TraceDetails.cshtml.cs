using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenTracerPackage.Core.Dto;
using OpenTracerPackage.Core.Entities;
using OpenTracerPackage.Infra;
using System.Text.Json;
using WebAPI.Models;

namespace WebAPI.Pages
{
    public class TraceDetailsModel : PageModel
    {
        [FromQuery]
        public string Id { get; set; }
        public Traces Trace { get; set; }
        public TraceMap TraceMap { get; set; } = new();
        private readonly AppDbContext _appDataAccess;
        public int TotalPages { get; set; } = 1;
        [FromQuery]
        public int? Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public TraceDetailsModel(AppDbContext appDataAccess)
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
            TotalPages = (int)Math.Round((float)TraceMap.Details.Count / (float) PageSize);
        }
    }
}
