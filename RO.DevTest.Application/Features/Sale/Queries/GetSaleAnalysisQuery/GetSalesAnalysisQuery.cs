using MediatR;

namespace RO.DevTest.Application.Features.Sale.Queries.GetSaleAnalysisQuery
{
    public class GetSalesAnalysisQuery : IRequest<GetSalesAnalysisResult>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}

