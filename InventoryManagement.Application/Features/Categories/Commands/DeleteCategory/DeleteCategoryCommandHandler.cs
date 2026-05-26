using Common.Interfaces.Products.Query;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Categories.Command;
using InventoryManagement.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Features.Categories.Commands.DeleteCategory
{
    public sealed class DeleteCategoryCommandHandler: IRequestHandler<DeleteCategoryCommand, OperationResult>
    {
        private readonly ICategoryCommandRepository _repository;
        private readonly IProductQueryRepository _productQueryRepository;

        public DeleteCategoryCommandHandler( ICategoryCommandRepository repository , IProductQueryRepository productQueryRepository)
        {
            _repository = repository;
            _productQueryRepository = productQueryRepository;
        }

        public async Task<OperationResult> Handle(DeleteCategoryCommand request,CancellationToken cancellationToken)
        {

            var hasProducts = await _productQueryRepository.ExistsByCategoryIdAsync(request.Id, cancellationToken);

            if (hasProducts)
                throw new BusinessException("La categoria no puede ser eliminada por que ya se encuentra asignada a otro producto.");

            var deleted = await _repository.DeleteAsync( request.Id, cancellationToken);

            if (!deleted)   
                throw new NotFoundException("Categoria no encontrada.");

            return new OperationResult(true, "La categoria fue borrada satisfactoriamente.");
        }
    }
}
