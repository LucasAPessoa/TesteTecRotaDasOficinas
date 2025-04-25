using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Products.Queries.GetProductByIdQuery;

namespace RO.DevTest.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
                throw new ArgumentException("Produto não encontrado.");

            return new GetProductByIdResult(product);
        }
    }
}