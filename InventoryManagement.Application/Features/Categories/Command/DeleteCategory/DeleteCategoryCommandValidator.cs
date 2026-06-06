using FluentValidation;

namespace InventoryManagement.Application.Features.Categories.Command.DeleteCategory
{
    public sealed class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("El id de categoria es requerido.");

        }

    }
}
