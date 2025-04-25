using FluentValidation;
using RO.DevTest.Application.Features.Products.Queries.GetAllProductsQuery;

namespace RO.DevTest.Application.Validators.Products
{
    public class GetAllProductsQueryValidator : AbstractValidator<GetAllProductsQuery>
    {
        private readonly List<string> validSortFields = new() { "Name", "Price", "Description" };

        public GetAllProductsQueryValidator()
        {
            RuleFor(q => q.SortDirection)
                .Must(sd => string.IsNullOrEmpty(sd) || sd.ToLower() == "asc" || sd.ToLower() == "desc")
                .WithMessage("SortDirection deve ser 'asc' ou 'desc'.");

            RuleFor(q => q.SortBy)
                .Must(sortBy => string.IsNullOrEmpty(sortBy) || validSortFields.Contains(sortBy))
                .WithMessage("SortBy deve ser um campo válido (Name, Price ou Description).");

            RuleFor(q => q)
                .Must(q => !q.MinPrice.HasValue || !q.MaxPrice.HasValue || q.MinPrice <= q.MaxPrice)
                .WithMessage("MinPrice não pode ser maior que MaxPrice.");

            RuleFor(q => q.Pagination.PageNumber)
                .GreaterThan(0)
                .WithMessage("PageNumber deve ser maior que zero.");

            RuleFor(q => q.Pagination.PageSize)
                .GreaterThan(0)
                .WithMessage("PageSize deve ser maior que zero.");
        }
    }
}
