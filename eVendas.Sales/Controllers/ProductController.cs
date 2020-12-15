using System.Linq;
using AutoMapper;
using eVendas.Sales.Dto;
using eVendas.Sales.Interface;
using eVendas.Sales.Model;
using Microsoft.AspNetCore.Mvc;

namespace eVendas.Sales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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
        public IActionResult Create([FromBody] ProductDto productDto)
        {
            var product = _mapper.Map<ProductDto, Product>(productDto);
            
            return Ok(_service.Create(_mapper.Map<ProductDto, Product>(productDto)));
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] ProductDto productDto)
        {
            var result = _service.Update(id, _mapper.Map<ProductDto, Product>(productDto));
            
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