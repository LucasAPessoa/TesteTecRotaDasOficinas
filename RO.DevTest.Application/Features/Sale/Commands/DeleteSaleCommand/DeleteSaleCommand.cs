using MediatR;

namespace RO.DevTest.Application.Features.Sale.Commands.DeleteSaleCommand
{
    public class DeleteSaleCommand : IRequest<DeleteSaleResult>
    {
        public Guid Id { get; set; }
        public DeleteSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}

