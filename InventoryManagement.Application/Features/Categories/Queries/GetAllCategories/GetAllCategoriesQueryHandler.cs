
using InventoryManagement.Application.Common.Interfaces.Categories.Queries;
using MediatR;


namespace InventoryManagement.Application.Features.Categories.Queries.GetAllCategories
{
    public sealed class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryResponse>>
    {
        private readonly ICategoryQueryRepository _repository;

        public GetAllCategoriesQueryHandler(ICategoryQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CategoryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllAsync(cancellationToken);

            return categories.Select(x => new CategoryResponse(
                x.Id,
                x.Name,
                x.CreatedAt
            )).ToList();
        }
    }
}
