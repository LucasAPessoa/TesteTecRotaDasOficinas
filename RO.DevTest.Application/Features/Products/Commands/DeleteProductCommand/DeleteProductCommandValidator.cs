using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace RO.DevTest.Application.Features.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("O ID do usuário não pode ser vazio");
        }
    }
}
