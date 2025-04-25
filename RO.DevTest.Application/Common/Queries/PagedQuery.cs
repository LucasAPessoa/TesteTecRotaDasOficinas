using MediatR;
using RO.DevTest.Application.Common.Filters;

namespace RO.DevTest.Application.Common.Queries
{
    public abstract class PagedQuery<TResponse> : IRequest<TResponse>
    {
        public PaginationFilter Pagination { get; set; } = new();
    }
}