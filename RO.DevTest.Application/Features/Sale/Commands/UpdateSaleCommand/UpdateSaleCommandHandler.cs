using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Sales.Common;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Features.Sales.Commands.UpdateSaleCommand
{
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;

        public UpdateSaleCommandHandler(
            ISaleRepository saleRepository,
            IProductRepository productRepository)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id)
                       ?? throw new ArgumentException("Venda não encontrada");

            if (!string.IsNullOrWhiteSpace(request.UserId))
    sale.UserId = request.UserId;

if (request.Items != null)
{
    foreach (var itemDto in request.Items)
    {
        var existingItem = sale.SaleItems.FirstOrDefault(si => si.Product.Id == itemDto.ProductId);
        if (existingItem != null)
        {
            if (itemDto.Quantity > 0) 
                existingItem.Quantity = itemDto.Quantity;

            var product = await _productRepository.GetByIdAsync(itemDto.ProductId)
                          ?? throw new ArgumentException($"Produto com ID {itemDto.ProductId} não encontrado.");

            existingItem.Price = product.Price;
        }
        else
        {
            throw new ArgumentException($"Item da venda com ID {itemDto.ProductId} não encontrado.");
        }
    }
}

            sale.ModifiedOn = DateTime.UtcNow;

            await _saleRepository.SaveChangesAsync();

            return new UpdateSaleResult
            {
                Id = sale.Id,
                UserId = sale.User.Id,
                TotalItems = sale.TotalItems,
                TotalSalePrice = sale.TotalSalePrice,
                Items = sale.SaleItems.Select(item => new SaleItemDto
                {
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    CreatedOn = item.CreatedOn,
                    ModifiedOn = item.ModifiedOn
                }).ToList()
            };
        }
    }
}