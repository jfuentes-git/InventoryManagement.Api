
using InventoryManagement.Application.Common.Authentication;
using MediatR;


namespace InventoryManagement.Application.Features.Authentication.Command.Login
{
    public sealed record LoginCommand (string UserName,string Password) : IRequest<AuthResponse>;

}
