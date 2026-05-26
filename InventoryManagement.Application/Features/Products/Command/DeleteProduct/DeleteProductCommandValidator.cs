using FluentValidation;
using InventoryManagement.Application.Features.FeaturesCatalog.Command.DeleteProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.Product.Command.DeleteProduct
{
    public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
