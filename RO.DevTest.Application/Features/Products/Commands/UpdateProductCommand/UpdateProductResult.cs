using ProductEntity = RO.DevTest.Domain.Entities.Product;

namespace RO.DevTest.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductResult
    {
        public Guid Id { get; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }

        public UpdateProductResult() { }

        public UpdateProductResult(ProductEntity product)
        {
            if (product.Id == Guid.Empty)
                throw new ArgumentNullException(nameof(product.Id), "Id é obrigatório");

            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;

        }
    }
}