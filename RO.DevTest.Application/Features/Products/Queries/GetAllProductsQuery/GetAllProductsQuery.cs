using RO.DevTest.Application.Common.Queries;

namespace RO.DevTest.Application.Features.Products.Queries.GetAllProductsQuery
{
    public class GetAllProductsQuery : PagedQuery<List<GetAllProductsResult>>
    {
        public string? FilterBy { get; set; }
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }


}

 