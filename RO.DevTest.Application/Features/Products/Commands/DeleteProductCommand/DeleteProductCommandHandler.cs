using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;

namespace RO.DevTest.Application.Features.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteProductResult>
    {
       private readonly IProductRepository _productRepository;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product is null)
            {
                return new DeleteProductResult
                {
                    Succeeded = false,
                    Errors = new List<string> { "Produto não encontrado." },
                };
            }
            await _productRepository.DeleteAsync(product.Id);

            return new DeleteProductResult
            {
                Succeeded = true,
                Message = "Produto deletado com sucesso.",
            };
        }
    }
}
