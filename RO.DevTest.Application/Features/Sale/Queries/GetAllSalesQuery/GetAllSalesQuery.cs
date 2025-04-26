using RO.DevTest.Application.Common.Queries;

namespace RO.DevTest.Application.Features.Sale.Queries.GetAllSalesQuery
{
    public class GetAllSalesQuery : PagedQuery<List<GetAllSalesResult>>
    {
        public string? FilterBy { get; set; }
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
    }
}