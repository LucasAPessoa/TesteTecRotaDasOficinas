using MediatR;
using RO.DevTest.Application.Features.Products.Queries.GetProductByIdQuery;

namespace RO.DevTest.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<GetProductByIdResult>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}