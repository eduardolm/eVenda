using eVendas.Warehouse.Interface;
using Microsoft.AspNetCore.Mvc;

namespace eVendas.Warehouse.Controller.GenericController
{
    [ApiController]
    public class GenericController<T> : ControllerBase where T : class, IBase
    {
        private readonly IGenericService<T> _service;

        protected GenericController(IGenericService<T> service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] T entity)
        {
            return Ok(_service.Create(entity));
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] T entity)
        {
            return Ok(_service.Update(id, entity));
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return Ok(_service.Delete(id));
        }
    }
}