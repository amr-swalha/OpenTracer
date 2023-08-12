using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using OpenTracerPackage.Core.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<T> : ControllerBase where T : EntityRoot
    {
        private readonly IRootService<T> _service;
        public BaseController(IRootService<T> service)
        {
            _service = service;
        }
        [HttpGet]
        [EnableQuery]
        public virtual IActionResult Get()
        {
            return Ok(_service.Query().AsNoTracking().AsSplitQuery());
        }
        [HttpPost]
        public virtual async Task<IActionResult> Post(T entity)
        {
            var result = await _service.InsertAsync(entity);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public virtual async Task<IActionResult> InsertList(List<T> entity)
        {
            await _service.InsertAsync(entity);
            return Ok();
        }
        [HttpPut]
        public virtual IActionResult Put(T entity)
        {
            var result = _service.Update(entity);
            return Ok(result.Entity);
        }
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(string id)
        {
            _service.Delete(new Guid(id));
            return Ok();
        }
    }
}
