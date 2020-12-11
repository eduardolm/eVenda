using System.Linq;
using eVendas.Warehouse.Context;
using eVendas.Warehouse.Model;
using FluentValidation;

namespace eVendas.Warehouse.Validator
{
    public class ProductValidator : AbstractValidator<Product>
    {
        private readonly MainContext _context;

        public ProductValidator(MainContext context)
        {
            _context = context;

            RuleFor(x => x.Sku)
                .NotEmpty()
                .WithMessage("Código não pode ser deixado em branco.")
                .NotNull()
                .WithMessage("Código não pode ser deixado em branco.")
                .Length(4, 20)
                .WithMessage("O código deve ter entre 3 e 20 caracteres.");
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Nome não pode ser deixado em branco.")
                .Length(3, 50)
                .WithMessage("O nome deve ter entre 3 e 50 caracteres.");
            
            RuleFor(x => x.Price)
                .NotNull()
                .WithMessage("Preço nao pode ser nulo.")
                .NotEmpty()
                .WithMessage("Preço não pode ser deixado em branco.")
                .Must(x => x > 0)
                .WithMessage("Preço deve obrigatoriamente ser maior que zero.");
                
            RuleFor(x => x.Quantity)
                .NotNull()
                .WithMessage("Quantidade nãoo pode ser nula.")
                .NotEmpty()
                .WithMessage("Quantidade não pode ser deixado em branco.")
                .Must(x => x >= 0)
                .WithMessage("A quantidade informada deve ser maior ou igual a zero.");

            RuleFor(x => x)
                .Must(x => !IsDuplicate(x))
                .WithMessage("Produto já cadastrado.");
        }

        private bool IsDuplicate(Product product)
        {
            var compProduct = (from n in _context.Products.ToList() select n);
            return compProduct.Any(x => x.Name == product.Name || x.Sku == product.Sku);
        }
    }
}