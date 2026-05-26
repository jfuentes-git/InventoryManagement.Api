using FluentValidation;
using InventoryManagement.Application.Features.FeaturesCatalog.Command.UpdateProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.Products.Command.UpdateProduct
{
public sealed class UpdateProductCommandValidator: AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
              RuleFor(x => x.Id)
             .NotEmpty();

              RuleFor(x => x.Name)
             .NotEmpty()
             .MaximumLength(150);

              RuleFor(x => x.Price)
             .GreaterThanOrEqualTo(0);

              RuleFor(x => x.CategoryId)
             .NotEmpty();

        }
}
}
