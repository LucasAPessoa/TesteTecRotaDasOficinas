using RO.DevTest.Application.Features.Sales.Common;

namespace RO.DevTest.Application.Features.Sales.Commands.UpdateSaleCommand
{
    public class UpdateSaleResult
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public decimal TotalSalePrice { get; set; }
        public int TotalItems { get; set; }

        public List<SaleItemDto> Items { get; set; }
    

    
    } 
}