using ProductEntity = RO.DevTest.Domain.Entities.Product;

namespace RO.DevTest.Application.Features.Products.Queries.GetProductByIdQuery
{
    public class GetProductByIdResult
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public GetProductByIdResult(ProductEntity product) {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
        }

    }
}
