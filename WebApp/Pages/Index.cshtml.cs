using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDataAccess _appDataAccess;
    public List<Traces> Traces { get; set; } = new();
    public IndexModel(ILogger<IndexModel> logger, AppDataAccess appDataAccess)
    {
        _logger = logger;
        _appDataAccess = appDataAccess;
    }

    public void OnGet()
    {
        Traces = _appDataAccess.Set<Traces>().Take(10).ToList();
    }
}