using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Products.Commands.UpdateProduct;

namespace RO.DevTest.Application.Features.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
                throw new KeyNotFoundException("Produto não encontrado.");

            if (!string.IsNullOrWhiteSpace(request.Name))
                product.Name = request.Name;

            if (!string.IsNullOrWhiteSpace(request.Description))
                product.Description = request.Description;

            if (request.Price.HasValue)
                product.Price = request.Price.Value;

            product.ModifiedOn = DateTime.UtcNow;

            await _productRepository.UpdateAsync(product);

            return new UpdateProductResult(product);
        }
    }
}