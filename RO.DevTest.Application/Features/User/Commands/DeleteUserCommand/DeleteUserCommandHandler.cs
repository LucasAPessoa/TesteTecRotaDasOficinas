using FluentValidation.Results;
using MediatR;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.User.Commands.DeleteUserCommand
{
    public class DeleteUserCommandHandler(IIdentityAbstractor identityAbstractor) : IRequestHandler<DeleteUserCommand, DeleteUserResult>
    {
        private readonly IIdentityAbstractor _identityAbstractor = identityAbstractor;

        public async Task<DeleteUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            DeleteUserCommandValidator validator = new();
            ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult);
            }

            var user = await _identityAbstractor.FindUserByIdAsync(request.Id);
            if (user is null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }
            var result = await _identityAbstractor.DeleteUser(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new ArgumentException($"Erro ao deletar o usuário: {errors}");
            }
            return new DeleteUserResult(result);
        }
    }
}
