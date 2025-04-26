using FluentValidation;

namespace RO.DevTest.Application.Features.User.Commands.UpdateUserCommand
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("O Id do usuário não pode ser vazio");

            RuleFor(x => x.Name)
                .MinimumLength(3)
                .WithMessage("O nome do usuário deve ter pelo menos 3 caracteres")
                .When(x => !string.IsNullOrWhiteSpace(x.Name));

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("O nome de usuário não pode ser vazio")
                .MinimumLength(3)
                .WithMessage("O nome de usuário deve ter pelo menos 3 caracteres")
                .When(x => !string.IsNullOrWhiteSpace(x.UserName));

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("O email do usuário deve ser um email válido")
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.PhoneNumber)
                .NotNull() 
                .NotEmpty()
                .WithMessage("O número de telefone deve conter apenas números")
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
        }
    }
}