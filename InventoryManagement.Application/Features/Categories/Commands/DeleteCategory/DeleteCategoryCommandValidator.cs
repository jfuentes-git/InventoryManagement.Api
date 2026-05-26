using FluentValidation;

namespace InventoryManagement.Application.Features.Categories.Commands.DeleteCategory
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
