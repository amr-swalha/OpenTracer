using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenTracer.Core.Entities;
using OpenTracer.Infra;

namespace WebAPI.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext _appDataAccess;
    public List<Traces> Traces { get; set; } = new();
    public int TotalPages { get; set; } = 1;
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 3;
    [FromQuery]
    public int? Page { get; set; }
    [BindProperty]
    public string? Id { get; set; }
    public IndexModel(ILogger<IndexModel> logger, AppDbContext appDataAccess)
    {
        _logger = logger;
        _appDataAccess = appDataAccess;
    }

    public void OnGet()
    {
        TotalPages = (int)Math.Round(((float)_appDataAccess.Set<Traces>().Count() / (float)PageSize));
        if (Page == null)
        {
            Traces = _appDataAccess.Set<Traces>().OrderByDescending(y => y.CreationDate).Take(PageSize).ToList();
            Page = 1;
        }
        else
            Traces = _appDataAccess.Set<Traces>().OrderByDescending(y => y.CreationDate).Skip((Page.Value - 1) * PageSize).Take(PageSize).ToList();
    }

    public void DeleteAll()
    {
        _appDataAccess.Set<Traces>().Where(y => y.CreationDate < DateTime.UtcNow).ExecuteDelete();
    }
    public IActionResult OnPostDeleteItem()
    {
        var test = new Guid(Id);
        _appDataAccess.Set<Traces>().Where(y => y.Id == test).ExecuteDelete();
        return Redirect("/");
    }
}