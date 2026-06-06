
using FluentValidation;

namespace InventoryManagement.Application.Features.Categories.Queries.GetCategoryById
{
    public sealed class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("El id de categoria es requerido.");
        }
    }
}
