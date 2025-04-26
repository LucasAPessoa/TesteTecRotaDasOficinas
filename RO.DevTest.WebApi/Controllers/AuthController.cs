using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RO.DevTest.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using RO.DevTest.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace RO.DevTest.WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthController(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            Console.WriteLine("Usuário não encontrado.");
            return Unauthorized(new { Message = "Email ou senha inválidos." });
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        Console.WriteLine($"Usuário: {user.Email}, Senha Válida: {isPasswordValid}");

        if (!isPasswordValid)
        {
            return Unauthorized(new { Message = "Email ou senha inválidos." });
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
        return Ok(new { Token = token });
    }
}
