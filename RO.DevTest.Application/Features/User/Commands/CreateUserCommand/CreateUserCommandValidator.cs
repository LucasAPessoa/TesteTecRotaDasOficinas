using FluentValidation;

namespace RO.DevTest.Application.Features.User.Commands.CreateUserCommand;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>{
    public CreateUserCommandValidator() {
        RuleFor(cpau => cpau.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo e-mail precisa ser preenchido");

        RuleFor(cpau => cpau.Email)
            .EmailAddress()
            .WithMessage("O campo e-mail precisa ser um e-mail válido");

        RuleFor(cpau => cpau.Password)
            .MinimumLength(6)
            .WithMessage("O campo senha precisa ter, pelo menos, 6 caracteres");

        RuleFor(cpau => cpau.PasswordConfirmation)
            .Equal(cpau => cpau.Password)
            .WithMessage("O campo de confirmação de senha deve ser igual ao campo senha");
        RuleFor(x => x.PhoneNumber)
               .NotEmpty();
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("O nome de usuário não pode ser vazio")
            .MinimumLength(3)
            .WithMessage("O nome de usuário deve ter pelo menos 3 caracteres");
    }
}
