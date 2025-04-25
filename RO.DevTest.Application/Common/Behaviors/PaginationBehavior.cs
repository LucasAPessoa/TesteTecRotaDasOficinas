using MediatR;
using RO.DevTest.Application.Common.Filters;
using RO.DevTest.Application.Common.Models;
using RO.DevTest.Application.Common.Queries;

namespace RO.DevTest.Application.Common.Behaviors
{
    public class PaginationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (request is not PagedQuery<TResponse> pagedQuery)
            {
                return await next();
            }

            var result = await next();

            if (result is IQueryable<object> queryable)
            {
                var page = pagedQuery.Pagination.PageNumber;
                var size = pagedQuery.Pagination.PageSize;

                var paginated = PaginatedList<object>.Create(queryable, page, size);
                return (TResponse)(object)paginated;
            }

            return result;
        }
    }
}