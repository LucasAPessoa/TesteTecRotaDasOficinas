using MediatR;
using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Sales.Common;
using System.Linq.Dynamic.Core;

namespace RO.DevTest.Application.Features.Sale.Queries.GetAllSalesQuery
{
    public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, List<GetAllSalesResult>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly SaleItemService _saleItemService;

        public GetAllSalesQueryHandler(ISaleRepository saleRepository, SaleItemService saleItemService)
        {
            _saleRepository = saleRepository;
            _saleItemService = saleItemService;
        }

        public async Task<List<GetAllSalesResult>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            var query = _saleRepository
                .GetAllAsQueryable()
                .Include(s => s.User)
                .Include(s => s.SaleItems)
                    .ThenInclude(i => i.Product)
                .AsQueryable();


            if (!string.IsNullOrEmpty(request.FilterBy))
            {
                var filter = request.FilterBy.ToLower();
                query = query.Where(e => e.UserId != null && e.UserId.ToLower().Contains(filter));
            }

            if (request.MinPrice.HasValue)
                query = query.Where(p => p.TotalSalePrice >= request.MinPrice.Value);

            if (request.MaxPrice.HasValue)
                query = query.Where(p => p.TotalSalePrice <= request.MaxPrice.Value);

            if (request.MinQuantity.HasValue)
                query = query.Where(p => p.TotalItems >= request.MinQuantity.Value);

            if (request.MaxQuantity.HasValue)
                query = query.Where(p => p.TotalItems <= request.MaxQuantity.Value);

            if (!string.IsNullOrWhiteSpace(request.Pagination.SortBy))
            {
                var sortExpression = $"{request.Pagination.SortBy} {request.Pagination.SortDirection}";
                query = query.OrderBy(sortExpression);
            }
            else if (!string.IsNullOrEmpty(request.SortBy))
            {
                if (request.SortDirection == "asc")
                {
                    query = query.OrderBy(e => EF.Property<object>(e, request.SortBy));
                }
                else if (request.SortDirection == "desc")
                {
                    query = query.OrderByDescending(e => EF.Property<object>(e, request.SortBy));
                } else
                {
                    query = query.OrderBy(e => e.CreatedOn);
                }
            }

            var projected = query.Select(sale => new GetAllSalesResult
            {
                Id = sale.Id,
                UserId = sale.User.Id,
                TotalItems = sale.TotalItems,
                TotalSalePrice = sale.TotalSalePrice,
                Items = sale.SaleItems.Select(i => new SaleItemDto
                {
                    ProductId = i.Product.Id,
                    Quantity = i.Quantity,
                    Price = i.Product.Price,
                    CreatedOn = i.Product.CreatedOn,
                    ModifiedOn = i.Product.ModifiedOn,
                }).ToList()
            });

            var result = await projected
                .Skip((request.Pagination.PageNumber - 1) * request.Pagination.PageSize)
                .Take(request.Pagination.PageSize)
                .ToListAsync(cancellationToken);

            return result;
        }

    }
}
