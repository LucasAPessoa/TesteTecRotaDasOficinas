
using MediatR;
using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistance.Repositories;

namespace RO.DevTest.Application.Features.Sale.Queries.GetSaleAnalysisQuery
{
    public class GetSalesAnalysisQueryHandler : IRequestHandler<GetSalesAnalysisQuery, GetSalesAnalysisResult>
    {
        private readonly ISaleRepository _saleRepository;

        public GetSalesAnalysisQueryHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<GetSalesAnalysisResult> Handle(GetSalesAnalysisQuery request, CancellationToken cancellationToken)
        {
            var startUtc = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
            var endUtc = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);

            var sales = await _saleRepository
                .GetAllAsQueryable()
                .Where(s => s.CreatedOn >= startUtc && s.CreatedOn <= endUtc)
                .Include(s => s.SaleItems)
                    .ThenInclude(si => si.Product)
                .ToListAsync(cancellationToken);

            var totalRevenue = sales.Sum(s => s.TotalSalePrice);
            var totalSales = sales.Count;

            var productsRevenue = sales
                .SelectMany(s => s.SaleItems)
                .GroupBy(i => new { i.Product.Id, i.Product.Name })
                .Select(g => new GetSalesAnalysisResult.ProductRevenue
                {
                    ProductId = g.Key.Id,
                    ProductName = g.Key.Name,
                    TotalRevenue = g.Sum(i => i.Price * i.Quantity),
                    TotalSold = g.Sum(i => i.Quantity)
                })
                .ToList();

            return new GetSalesAnalysisResult
            {
                TotalSales = totalSales,
                TotalRevenue = totalRevenue,
                ProductRevenues = productsRevenue
            };
        }
    }
}
