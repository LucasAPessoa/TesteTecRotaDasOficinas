using MediatR;
using RO.DevTest.Application.Features.Products.Commands.UpdateProduct;

namespace RO.DevTest.Application.Features.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommand : IRequest<UpdateProductResult>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }
}

