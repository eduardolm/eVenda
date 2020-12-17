using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Create([FromBody] Sale sale)
        {
            return Ok(await _service.Create(sale));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Sale sale)
        {
            var result = await _service.Update(id, sale);
            
            if (result != null) return Ok(result);
            return BadRequest(new {Message = "Ocorreu um erro ao processar a sua solicitação. Verifique " +
                                             "os dados informados."});
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.Delete(id));
        }
    }
}