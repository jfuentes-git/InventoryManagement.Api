
using InventoryManagement.Application.Common.Authentication;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces.Authentication;
using MediatR;


namespace InventoryManagement.Application.Features.Authentication.Command.Login
{
    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {

    
        private readonly IAuthService _authenticationService;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;
        public LoginCommandHandler(IJwtTokenGenerator tokenGenerator,
            IAuthService authenticationService ,IUserRepository userRepository)
        {
            _tokenGenerator = tokenGenerator;
            _authenticationService = authenticationService;
            _userRepository = userRepository;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {


            var userFromDatabase = await _userRepository.ObtenerPorUserNameAsync(request.UserName);

            if (userFromDatabase is null || !userFromDatabase.IsActive)
                throw new UnauthorizedException("Usuario o contraseña incorrectos");


            var isValid = await _authenticationService.ValidateCredentials(userFromDatabase, request.Password);

            if (!isValid)
                throw new UnauthorizedException("Usuario o contraseña incorrectos");

            var tokenGenerated = await _tokenGenerator.GenerateToken(userFromDatabase);

            return new AuthResponse( tokenGenerated.AccessToken,tokenGenerated.ExpiresAt );

        }

    }

}
