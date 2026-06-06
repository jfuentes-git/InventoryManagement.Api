using InventoryManagement.Application.Common.Models;
using InventoryManagement.Application.Features.Categories.Command.CreateCategory;
using InventoryManagement.Application.Features.Categories.Command.DeleteCategory;
using InventoryManagement.Application.Features.Categories.Command.UpdateCategory;
using InventoryManagement.Application.Features.Categories.Queries.GetAllCategories;
using InventoryManagement.Application.Features.Categories.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[Authorize]
[ApiController]
[Route("api/Categories")]
[Produces("application/json")]
public sealed class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene el listado de todas las categorías registradas.
    /// </summary>
    /// <remarks>
    /// Devuelve todas las categorías sin filtros ni paginación.
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllCategoriesQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Obtiene una categoría específica por su identificador.
    /// </summary>
    /// <param name="id">Identificador único de la categoría.</param>
    /// <remarks>
    /// Si la categoría no existe, se devuelve un error 404.
    /// </remarks>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Registra una nueva categoría en el sistema.
    /// </summary>
    /// <param name="command">Comando con los datos necesarios para crear la categoría.</param>
    /// <remarks>
    /// Permite crear una categoría con su información básica como nombre y descripción.
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(CreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create(
        [FromBody] CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Actualiza una categoría existente.
    /// </summary>
    /// <param name="command">Comando con los datos actualizados de la categoría.</param>
    /// <remarks>
    /// Reemplaza la información actual de la categoría con los datos enviados en la solicitud.
    /// </remarks>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromBody] UpdateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Elimina una categoría del sistema.
    /// </summary>
    /// <param name="id">Identificador único de la categoría a eliminar.</param>
    /// <remarks>
    /// La eliminación puede fallar si la categoría está asociada a productos u otros registros.
    /// </remarks>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]

    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteCategoryCommand(id), cancellationToken);
        return NoContent();
    }
}