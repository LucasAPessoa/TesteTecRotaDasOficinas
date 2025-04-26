using RO.DevTest.Application.Features.Sales.Common;
using SaleEntity = RO.DevTest.Domain.Entities.Sale;

namespace RO.DevTest.Application.Features.Sale.Commands.CreateSaleCommand;

public class CreateSaleResult
{
    public Guid Id { get; set; }
    public List<SaleItemDto> Items { get; set; }
    public decimal TotalSalePrice => Items.Sum(item => item.Quantity * item.Price);
    public int TotalItems => Items.Sum(item => item.Quantity);

    public CreateSaleResult(SaleEntity sale)
    {
        Id = sale.Id;
       Items = sale.SaleItems.Select(item => new SaleItemDto
        {
            ProductId = item.Product.Id,
            Quantity = item.Quantity,
            Price = item.Price
        }).ToList();

    }
}