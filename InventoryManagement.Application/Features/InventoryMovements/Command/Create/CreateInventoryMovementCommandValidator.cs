
using FluentValidation;
using InventoryManagement.Domain.Enums;

namespace InventoryManagement.Application.Features.InventoryMovements.Command.Create;

    public sealed class CreateInventoryMovementCommandValidator: AbstractValidator<CreateInventoryMovementCommand>
    {
        public CreateInventoryMovementCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("El Id de Producto es Requerido");

            RuleFor(x => x.MovementType)
                .IsInEnum()
                .WithMessage("Id de Movimiento Invalido");

            RuleFor(x => x)
                .Must(x => (x.MovementType == MovementType.Entry || x.MovementType == MovementType.Exit) ? x.Quantity > 0: true)
                .WithMessage("Los movimientos de entrada y salida no pueden ser negativos o iguales a 0");
        }
    }



