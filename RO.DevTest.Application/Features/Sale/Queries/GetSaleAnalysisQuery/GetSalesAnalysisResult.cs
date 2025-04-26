namespace RO.DevTest.Application.Features.Sale.Queries.GetSaleAnalysisQuery
{
    public class GetSalesAnalysisResult
    {
        public int TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<ProductRevenue> ProductRevenues { get; set; } = new(); 

        public class ProductRevenue
        {
            public Guid ProductId { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public decimal TotalRevenue { get; set; }
            public int TotalSold { get; set; }
        }
    }
}
