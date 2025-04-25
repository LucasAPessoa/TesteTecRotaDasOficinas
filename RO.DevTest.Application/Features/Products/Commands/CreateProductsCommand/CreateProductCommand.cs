using MediatR;

namespace RO.DevTest.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<CreateProductResult>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public Domain.Entities.Product AssignTo()
        {
            return new Domain.Entities.Product
            {
                Name = Name,
                Description = Description,
                Price = Price,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };
        }
    }
}