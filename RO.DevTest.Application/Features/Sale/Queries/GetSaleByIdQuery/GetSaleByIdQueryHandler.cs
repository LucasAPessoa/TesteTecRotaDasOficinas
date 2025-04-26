using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Sales.Commands.UpdateSaleCommand;
using RO.DevTest.Application.Features.Sales.Common;

namespace RO.DevTest.Application.Features.Sales.Queries.GetSaleById
{
    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;

        public GetSaleByIdQueryHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<UpdateSaleResult> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id)
                       ?? throw new ArgumentException("Venda não encontrada.");

            return new UpdateSaleResult
            {
                Id = sale.Id,
                TotalItems = sale.TotalItems,
                TotalSalePrice = sale.TotalSalePrice,
                Items = sale.SaleItems.Select(item => new SaleItemDto
                {
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };
        }
    }
}