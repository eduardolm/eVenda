using System.Linq;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using Microsoft.AspNetCore.Mvc;

namespace eVendas.Sales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _service;

        public SaleController(ISaleService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _service.GetAll();

            if (!result.Any()) return NotFound(new {Message = "Registros não encontrados."});
            
            return Ok(_service.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            if (result != null) return Ok(result);
            return NotFound(new {Message = "Produto não encontrado."});
        }

        [HttpPost]
        public IActionResult Create([FromBody] Sale sale)
        {
            return Ok(_service.Create(sale));
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Sale sale)
        {
            var result = _service.Update(id, sale);
            
            if (result != null) return Ok(result);
            return BadRequest(new {Message = "Ocorreu um erro ao processar a sua solicitação. Verifique " +
                                             "os dados informados."});
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return Ok(_service.Delete(id));
        }
    }
}