using FluentValidation;
using InventoryManagement.Application.Features.Categories.Commands.DeleteCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator: AbstractValidator<UpdateCategoryCommand>
    {
       public UpdateCategoryCommandValidator()
       {

            RuleFor(x => x.Id)
           .NotEmpty();

            RuleFor(x => x.Name)
           .NotEmpty()
           .MaximumLength(100);

        }

    }
}
