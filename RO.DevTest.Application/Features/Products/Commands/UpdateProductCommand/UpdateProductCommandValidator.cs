using FluentValidation;

namespace RO.DevTest.Application.Features.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O Id do produto é obrigatório.");

            When(x => x.Name is not null, () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("O nome não pode ser vazio.")
                    .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");
            });

            When(x => x.Price.HasValue, () =>
            {
                RuleFor(x => x.Price.Value)
                    .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");
            });
        }
    }
}