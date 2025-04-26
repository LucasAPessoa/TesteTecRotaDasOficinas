using MediatR;

namespace RO.DevTest.Application.Features.Auth.Commands.LoginCommand;

public class LoginCommand : IRequest<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
