namespace RO.DevTest.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }


        public CreateProductResult(Domain.Entities.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;

        }
    }
}