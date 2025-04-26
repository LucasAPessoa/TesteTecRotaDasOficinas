using FluentValidation;
using RO.DevTest.Application.Features.Sales.Common;

namespace RO.DevTest.Application.Features.Sales.Commands.UpdateSaleCommand
{
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleForEach(x => x.Items).SetValidator(new UpdateSaleItemValidator());
        }
    }

    public class UpdateSaleItemValidator : AbstractValidator<SaleItemDto>
    {
        public UpdateSaleItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}