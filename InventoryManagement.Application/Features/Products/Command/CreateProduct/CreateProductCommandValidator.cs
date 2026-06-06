
using FluentValidation;

namespace InventoryManagement.Application.Features.Products.Command.CreateProduct
{
    public sealed class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.CategoryId)
                .NotEmpty();
        }
    }
}
