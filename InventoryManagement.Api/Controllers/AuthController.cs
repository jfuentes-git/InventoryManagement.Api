
using InventoryManagement.Application.Common.Authentication;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Application.Features.Authentication.Command.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
           
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedException), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<object>),StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Login(LoginCommand Usuario)
        {
            var result = await _mediator.Send(Usuario);
            return Ok(result);
        }
    }
}