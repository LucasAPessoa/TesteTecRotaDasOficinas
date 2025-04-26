using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Features.Sales.Common;

public class SaleItemService
{
    private readonly IProductRepository _productRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;

    public SaleItemService(
        IProductRepository productRepository,
        ISaleRepository saleRepository,
        ISaleItemRepository saleItemRepository)
    {
        _productRepository = productRepository;
        _saleRepository = saleRepository;
        _saleItemRepository = saleItemRepository;
    }

    public async Task<List<SaleItem>> BuildSaleItemsAsync(List<SaleItem> items)
    {
        var result = new List<SaleItem>();

        foreach (var saleItem in items)
        {
            var product = await _productRepository.GetByIdAsync(saleItem.Product.Id)
                          ?? throw new ArgumentException($"Produto com ID {saleItem.Product} não encontrado.");

            result.Add(new SaleItem
            {
                Product = product,
                Quantity = saleItem.Quantity,
            });
        }

        return result;
    }

    public async Task<List<SaleItem>> UpdateSaleItemsAsync(Guid saleId, List<SaleItem> updatedItems)
    {

        var sale = await _saleRepository.GetByIdAsync(saleId);

        if (sale == null)
        {
            throw new ArgumentException($"Venda com ID {saleId} não encontrada.");
        }

        var saleItems = sale.SaleItems;

        if (saleItems.Count() != updatedItems.Count())
        {
            throw new ArgumentException($"A quantidade de itens na venda não confere com a quantidade de itens atualizados.");
        }

        foreach (var item in updatedItems)
        {
            var existingItem = saleItems.FirstOrDefault(i => i.Product.Id == item.Product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity = item.Quantity > 0 ? item.Quantity : existingItem.Quantity;

                await _saleItemRepository.UpdateAsync(existingItem);
            }
            else
            {
                throw new ArgumentException($"Produto com ID {item.Product.Id} não encontrado na venda.");
            }
        }

        return saleItems.ToList();
    }
}