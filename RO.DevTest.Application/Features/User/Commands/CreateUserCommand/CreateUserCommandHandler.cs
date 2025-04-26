using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.User.Commands.CreateUserCommand;

public class CreateUserCommandHandler(IIdentityAbstractor identityAbstractor) : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IIdentityAbstractor _identityAbstractor = identityAbstractor;

   

    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
  
        CreateUserCommandValidator validator = new();
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new BadRequestException(validationResult);
        }

        var existingUser = await _identityAbstractor.FindUserByEmailAsync(request.Email);
        if (existingUser is not null)
        {
            throw new ArgumentException("Já existe um usuário com esse e-mail.");
        }

        Domain.Entities.User newUser = request.AssignTo();
        IdentityResult userCreationResult = await _identityAbstractor.CreateUserAsync(newUser, request.Password);

        if (!userCreationResult.Succeeded)
        {
            var errors = string.Join(", ", userCreationResult.Errors.Select(e => e.Description));
            throw new ArgumentException($"Erro ao criar o usuário: {errors}");
        }

        IdentityResult userRoleResult = await _identityAbstractor.AddToRoleAsync(newUser, request.Role);
        if (!userRoleResult.Succeeded)
        {
            var errors = string.Join(", ", userRoleResult.Errors.Select(e => e.Description));
            throw new ArgumentException($"Erro ao criar o usuário: {errors}");
        }

        return new CreateUserResult(newUser);
    }
}
