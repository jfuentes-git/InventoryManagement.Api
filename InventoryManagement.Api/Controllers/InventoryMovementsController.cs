using InventoryManagement.Application.Features.InventoryMovements.Command;
using InventoryManagement.Application.Features.InventoryMovements.Queries.GetAllInventoryMovements;
using InventoryManagement.Application.Features.InventoryMovements.Queries.GetInventoryMovementByProductId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

/// <summary>
/// Controlador encargado de la gestión de movimientos de inventario.
/// Permite consultar movimientos, filtrar por producto y registrar nuevos movimientos.
/// </summary>
[Authorize]
[ApiController]
[Route("api/InventoryMovements")]
[Produces("application/json")]
public sealed class InventoryMovementsController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryMovementsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene todos los movimientos de inventario registrados en el sistema.
    /// </summary>
    /// <remarks>
    /// Este endpoint devuelve el historial completo de movimientos sin filtros.
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllInventoryMovementsQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Obtiene los movimientos de inventario asociados a un producto específico.
    /// </summary>
    /// <param name="productId">Identificador único del producto.</param>
    /// <remarks>
    /// Permite consultar todo el historial de entradas y salidas de un producto.
    /// Si el producto no tiene movimientos, se devuelve una lista vacía.
    /// </remarks>
    [HttpGet("product/{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByProductId(Guid productId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetInventoryMovementByProductIdQuery(productId),
            cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Registra un nuevo movimiento de inventario.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite registrar entradas o salidas de productos en el inventario.
    /// </remarks>
    /// <param name="command">Datos necesarios para crear el movimiento de inventario.
    /// </param>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateInventoryMovementCommand command,CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}