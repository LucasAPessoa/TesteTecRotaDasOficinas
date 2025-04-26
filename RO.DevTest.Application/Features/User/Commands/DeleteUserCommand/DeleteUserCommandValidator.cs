using FluentValidation;

namespace RO.DevTest.Application.Features.User.Commands.DeleteUserCommand
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("O ID do usuário não pode ser vazio");
        }
    }
    
    }

