using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;

namespace RO.DevTest.Application.Features.Sale.Commands.DeleteSaleCommand
{
    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleItemRepository _saleItemRepository;

        public DeleteSaleCommandHandler(ISaleRepository saleRepository, ISaleItemRepository saleItemRepository)
        {
            _saleRepository = saleRepository;
            _saleItemRepository = saleItemRepository;
        }

        public async Task<DeleteSaleResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id);
            if (sale == null)
            {
                return new DeleteSaleResult
                {
                    Succeeded = false,
                    Errors = new List<string> { "Venda não encontrada." },
                };
            }

            var saleItemIds = sale.SaleItems.Select(item => item.Id).ToList();
            if (saleItemIds.Any())
            {
                await _saleItemRepository.DeleteManyAsync(saleItemIds);
            }

            await _saleRepository.DeleteAsync(sale.Id);

            return new DeleteSaleResult
            {
                Succeeded = true,
                Message = "Venda deletada com sucesso.",
            };
        }
    }
}