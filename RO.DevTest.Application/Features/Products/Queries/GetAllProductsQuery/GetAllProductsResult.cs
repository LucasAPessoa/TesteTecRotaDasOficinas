using Microsoft.EntityFrameworkCore.Storage.Json;
using ProductEntity = RO.DevTest.Domain.Entities.Product; 

namespace RO.DevTest.Application.Features.Products.Queries.GetAllProductsQuery
{
    public class GetAllProductsResult
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public GetAllProductsResult() { }
        public GetAllProductsResult(ProductEntity product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            ModifiedOn = product.ModifiedOn;
            CreatedOn = product.CreatedOn;

        }
    }
}
