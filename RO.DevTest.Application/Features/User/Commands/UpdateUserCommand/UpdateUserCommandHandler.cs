using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using FluentValidation.Results;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.User.Commands.UpdateUserCommand
{
    public class UpdateUserCommandHandler(IIdentityAbstractor identityAbstractor) : IRequestHandler<UpdateUserCommand, UpdateUserResult>
    {

        private readonly IIdentityAbstractor _identityAbstractor = identityAbstractor;

        public async Task<UpdateUserResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {



            var validator = new UpdateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException($"Erro ao deletar o usuário: {errors}");
            }

            var user = await _identityAbstractor.FindUserByIdAsync(request.Id);
            if (user is null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                user.Name = request.Name;
            }
            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                user.UserName = request.UserName;
            }
            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                user.Email = request.Email;
            }
            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                user.PhoneNumber = request.PhoneNumber;
            }

            var result = await _identityAbstractor.UpdateUserAsync(user);
            if (result.Succeeded)
            {
                return new UpdateUserResult(user);
            }
            else
            {
                var errorMessages = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new ArgumentException($"Erro ao atualizar o usuário: {errorMessages}");
            }
        }

    }
}

