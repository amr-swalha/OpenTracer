using Microsoft.AspNetCore.Mvc;
using OpenTracer.Core.Abstraction;
using OpenTracer.Core.Entities;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : BaseController<Traces>
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITraceService _repository;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITraceService repository)
            :base(repository)
        {
            _logger = logger;
            _repository = repository;
        }


        [HttpGet("[action]")]
        public IActionResult Sample()
        {
            _repository.Delete(new Guid("75dd8b8f-63ff-4eac-867f-f6f86db3d013"));
            /*_repository.Update(new Traces { 
                Id = new Guid("75dd8b8f-63ff-4eac-867f-f6f86db3d013"),
                CreationDate = DateTime.UtcNow, Details = JsonSerializer.Serialize(new {Sample="data22"})
            });*/
            var data = _repository.Query().Where(y => y.CreationDate < DateTime.UtcNow).ToList();
            return Ok(data);
        }
    }
}