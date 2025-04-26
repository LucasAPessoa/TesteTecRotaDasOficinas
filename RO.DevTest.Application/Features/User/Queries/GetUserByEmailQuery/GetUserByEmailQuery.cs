using MediatR;

namespace RO.DevTest.Application.Features.User.Queries.GetUserByEmailQuery
{
    public class GetUserByEmailQuery : IRequest<GetUserByEmailResult>
    {

        public string Email { get; set; }

        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }

    }
}
