using InventoryManagement.Application.Common.Models;
using InventoryManagement.Application.Features.Products.Command.CreateProduct;
using InventoryManagement.Application.Features.Products.Command.DeleteProduct;
using InventoryManagement.Application.Features.Products.Command.UpdateProduct;
using InventoryManagement.Application.Features.Products.Queries.GetAllProducts;
using InventoryManagement.Application.Features.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[Authorize]
[ApiController]
[Route("api/Products")]
[Produces("application/json")]
public sealed class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;


    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene el listado de todos los productos registrados.
    /// </summary>
    /// <remarks>
    /// Devuelve todos los productos sin filtros ni paginación.
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllProductsQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Obtiene un producto específico por su identificador.
    /// </summary>
    /// <param name="id">Identificador único del producto.</param>
    /// <remarks>
    /// Si el producto no existe, se devuelve un error 404.
    /// </remarks>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Registra un nuevo producto en el sistema.
    /// </summary>
    /// <param name="command">Comando con los datos necesarios para crear el producto, debes de ingresar una categoria existente</param>
    /// <remarks>
    /// Incluye información como nombre, precio, categoría y stock inicial.
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(
        [FromBody] CreateProductCommand command,CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Actualiza un producto existente.
    /// </summary>
    /// <param name="command">Comando con los datos actualizados del producto.</param>
    /// <remarks>
    /// Reemplaza la información actual del producto con la enviada en el request.
    /// </remarks>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Elimina un producto del sistema.
    /// </summary>
    /// <param name="id">Identificador único del producto a eliminar.</param>
    /// <remarks>
    /// La eliminación puede fallar si el producto está asociado a movimientos de inventario.
    /// </remarks>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {

        await _mediator.Send(new DeleteProductCommand(id), cancellationToken);
        return NoContent();
    }
}