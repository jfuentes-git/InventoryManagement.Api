using Common.Authentication;
using InventoryManagement.Application.Common.Interfaces.Authentication;
using InventoryManagement.Application.Features.Authentication.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AuthController : ControllerBase
    {
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IAuthService _authenticationService;

        public AuthController(IJwtTokenGenerator tokenGenerator, IAuthService authenticationService)
        {
            _tokenGenerator = tokenGenerator;
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(UsuarioDTO Usuario)
        {
            var token = _tokenGenerator.GenerateToken(Usuario.UsserName);
            var usserVerified = await _authenticationService.ValidateCredentials(Usuario);

            if (!usserVerified)
                return Unauthorized();

            return Ok(
                    new LoginResponse(
                        token,
                        DateTime.UtcNow.AddHours(1)));
        }
    }
}