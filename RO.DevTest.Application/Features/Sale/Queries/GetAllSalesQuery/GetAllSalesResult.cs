using SaleEntity = RO.DevTest.Domain.Entities.Sale;
using RO.DevTest.Application.Features.Sales.Common;

namespace RO.DevTest.Application.Features.Sale.Queries.GetAllSalesQuery
{
    public class GetAllSalesResult
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public decimal TotalSalePrice { get; set; }
        public int TotalItems { get; set; }
        public List<SaleItemDto> Items { get; set; } = new();
        public DateTime CreatedOn { get; set; } 

        public GetAllSalesResult() { }

        public GetAllSalesResult(SaleEntity sale)
        {
            Id = sale.Id;
            UserId = sale.UserId;
            TotalSalePrice = sale.TotalSalePrice;
            TotalItems = sale.TotalItems;
            CreatedOn = sale.CreatedOn;
            if (sale.SaleItems != null)
            {
                foreach (var item in sale.SaleItems)
                {
                    Items.Add(new SaleItemDto
                    {
                        ProductId = item.Product.Id,
                        Quantity = item.Quantity,
                        Price = item.Price
                    });
                }
            }
        }

    }
}