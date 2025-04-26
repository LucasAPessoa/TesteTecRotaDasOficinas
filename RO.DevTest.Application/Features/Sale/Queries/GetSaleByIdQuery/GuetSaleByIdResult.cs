using RO.DevTest.Application.Features.Sales.Common;

namespace RO.DevTest.Application.Features.Sales.Queries.GetSaleById
{
    public class GetSaleByIdResult
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalSalePrice { get; set; }
        public List<SaleItemDto> Items { get; set; } = new();
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}