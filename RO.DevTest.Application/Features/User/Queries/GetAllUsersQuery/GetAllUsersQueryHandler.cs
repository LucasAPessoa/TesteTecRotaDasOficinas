using MediatR;
using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistance.Repositories;

namespace RO.DevTest.Application.Features.User.Queries.GetAllUsersQuery
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetAllUsersResult>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<GetAllUsersResult>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {

            var query = _userRepository.GetAllAsQueryable();

            if (!string.IsNullOrEmpty(request.FilterBy))
            {
                var filter = request.FilterBy.ToLower(); 

                query = query.Where(e =>
                   e.UserName != null && e.UserName.ToLower().Contains(filter) ||
                   e.Name != null && e.Name.ToLower().Contains(filter) ||
                   e.Email != null && e.Email.ToLower().Contains(filter)
   );
            }

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
                    query = query.OrderBy(e => e.UserName);
                }
            }

            var users = await query
                .Skip((request.Pagination.PageNumber - 1) * request.Pagination.PageSize)
                .Take(request.Pagination.PageSize)
                .ToListAsync(cancellationToken);

            return users.Select(user => new GetAllUsersResult(user)).ToList();
        }
    }
}