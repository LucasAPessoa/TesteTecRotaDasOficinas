using MediatR;
using RO.DevTest.Application.Features.Sales.Common;

namespace RO.DevTest.Application.Features.Sales.Commands.UpdateSaleCommand
{
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public List<SaleItemDto>? Items { get; set; }
    }


}