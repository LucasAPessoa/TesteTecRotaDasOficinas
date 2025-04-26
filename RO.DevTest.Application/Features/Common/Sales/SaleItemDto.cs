
using RO.DevTest.Domain.Abstract;

namespace RO.DevTest.Application.Features.Sales.Common;

public class SaleItemDto : BaseEntity
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}