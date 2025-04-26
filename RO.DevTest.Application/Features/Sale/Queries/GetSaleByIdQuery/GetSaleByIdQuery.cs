using MediatR;
using RO.DevTest.Application.Features.Sales.Commands.UpdateSaleCommand;

namespace RO.DevTest.Application.Features.Sales.Queries.GetSaleById
{
    public class GetSaleByIdQuery : IRequest<UpdateSaleResult>
    {
        public Guid Id { get; set; }

        public GetSaleByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}