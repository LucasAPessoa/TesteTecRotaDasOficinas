using FluentValidation;
using RO.DevTest.Application.Features.Sale.Commands.CreateSaleCommand;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.User)
            .NotEmpty().WithMessage("O usuário é obrigatório.");

        RuleFor(x => x.Items)
            .NotNull().WithMessage("A lista de itens não pode ser nula.")
            .Must(items => items.Any()).WithMessage("A venda deve conter pelo menos um item.");

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductId)
                .NotEmpty().WithMessage("O ID do produto é obrigatório.");

            item.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");
        });
    }
}