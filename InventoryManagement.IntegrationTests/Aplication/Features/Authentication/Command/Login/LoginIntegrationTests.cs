using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Authentication;
using InventoryManagement.Application.Common.Settings;
using InventoryManagement.Application.Features.Authentication.Command.Login;
using InventoryManagement.Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace InventoryManagement.IntegrationTests.Aplication.Features.Authentication.Command.Login;


public class LoginIntegrationTests
{
    [Fact]
    public async Task Login_WithInvalidPassword_ShouldNotGenerateToken()
    {

        var authSettings = Options.Create(
            new AuthSettings
            {
                UserName = "admin",
                Password = "aDtMlP.*##"
            });

        var jwtSettings = Options.Create(
            new JwtSettings
            {
                Key = "InventoryManagementSuperSecretKey202",
                Issuer = "InventoryManagement.Api",
                Audience = "InventoryManagement.Client",
                ExpirationMinutes = 60
            });

        IUserRepository userRepository = new UserRepository(authSettings);

        IAuthService authService =new AuthService();

        IJwtTokenGenerator tokenGenerator = new JwtTokenGenerator(jwtSettings);

        var handler = new LoginCommandHandler(tokenGenerator,authService,userRepository);

        var command = new LoginCommand("admin","passwordIncorrecto");

        await Assert.ThrowsAsync<UnauthorizedException>(
                    () => handler.Handle(
                          command,
                          CancellationToken.None));
    }
}