using FluentValidation;

namespace InventoryManagement.Application.Features.Categories.Command.UpdateCategory
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
