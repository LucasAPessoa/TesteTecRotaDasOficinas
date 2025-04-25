using MediatR;
using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistance.Repositories;

namespace RO.DevTest.Application.Features.Products.Queries.GetAllProductsQuery
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<GetAllProductsResult>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<GetAllProductsResult>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var query = _productRepository.GetAllAsQueryable();



            if (!string.IsNullOrEmpty(request.FilterBy))
            {
                var filter = request.FilterBy.ToLower();

                query = query.Where(e =>
                   e.Name != null && e.Name.ToLower().Contains(filter) ||
                   e.Description != null && e.Description.ToLower().Contains(filter) 
   );
            }

            if (request.MinPrice.HasValue)
                query = query.Where(p => p.Price >= request.MinPrice.Value);

            if (request.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= request.MaxPrice.Value);


            if (!string.IsNullOrEmpty(request.SortBy))
            {
                if (request.SortDirection == "asc")
                {
                    query = query.OrderBy(e => EF.Property<object>(e, request.SortBy));
                }
                else if (request.SortDirection == "desc")
                {
                    query = query.OrderByDescending(e => EF.Property<object>(e, request.SortBy));
                } else
                {
                    query = query.OrderBy(e => e.CreatedOn);
                }
            }
            var products = await query
                .Skip((request.Pagination.PageNumber - 1) * request.Pagination.PageSize)
                .Take(request.Pagination.PageSize)
                .ToListAsync();

            return products.Select(product => new GetAllProductsResult(product)).ToList();

        }
    }
    

    }
