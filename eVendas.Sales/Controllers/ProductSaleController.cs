using System.Linq;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using Microsoft.AspNetCore.Mvc;

namespace eVendas.Sales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductSaleController : ControllerBase
    {
        private readonly IProductSaleService _service;

        public ProductSaleController(IProductSaleService service)
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

        [HttpGet("{productId:int}/{saleId:int}")]
        public IActionResult GetById(int productId, int saleId)
        {
            var result = _service.GetById(productId, saleId);
            if (result != null) return Ok(result);
            return NotFound(new {Message = "Produto não encontrado."});
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductSale productSale)
        {
            return Ok(_service.Create(productSale));
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] ProductSale productSale)
        {
            var result = _service.Update(id, productSale);
            
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
