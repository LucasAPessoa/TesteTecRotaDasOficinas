using RO.DevTest.Application.Common.Queries;

namespace RO.DevTest.Application.Features.User.Queries.GetAllUsersQuery
{
    public class GetAllUsersQuery : PagedQuery<List<GetAllUsersResult>>
    {
        public string? FilterBy { get; set; }
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }
    }
}
