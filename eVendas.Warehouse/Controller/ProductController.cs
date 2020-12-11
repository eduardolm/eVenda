using AutoMapper;
using eVendas.Warehouse.Dto;
using eVendas.Warehouse.Interface;
using eVendas.Warehouse.Model;
using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Mvc;

namespace eVendas.Warehouse.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;
        private readonly IValidator<Product> _validator;

        public ProductController(IProductService service, IMapper mapper, IValidator<Product> validator)
        {
            _service = service;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
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
            var result = _validator.Validate(product);
            
            if (result.IsValid)
                return Ok(_service.Create(_mapper.Map<ProductDto, Product>(productDto)));
            return BadRequest(result.Errors.WithErrorMessage("Produto já cadastrado."));
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] ProductDto productDto)
        {
            var result = _service.Update(id, _mapper.Map<ProductDto, Product>(productDto));
            
            if (result != null) return Ok(result);
            return BadRequest(new {Message = "Ocorreu um erro ao tentar processar a sua solicitação. Verifique " +
                                             "os dados informados."});
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return Ok(_service.Delete(id));
        }
    }
}