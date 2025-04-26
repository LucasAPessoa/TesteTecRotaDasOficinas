using MediatR;
using Microsoft.AspNetCore.Identity;
using RO.DevTest.Application.Common.Interfaces;
using RO.DevTest.Application.Features.Auth.Commands;
using RO.DevTest.Application.Features.Auth.Commands.LoginCommand;
using UserEntity = RO.DevTest.Domain.Entities.User;

namespace RO.DevTest.Application.Features.Auth.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly SignInManager<UserEntity> _signInManager;

    public LoginCommandHandler(
        UserManager<UserEntity> userManager,
        IJwtTokenGenerator jwtTokenGenerator,
        SignInManager<UserEntity> signInManager)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _signInManager = signInManager;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Credenciais inválidas.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException("Credenciais inválidas.");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
        return token;
    }
}