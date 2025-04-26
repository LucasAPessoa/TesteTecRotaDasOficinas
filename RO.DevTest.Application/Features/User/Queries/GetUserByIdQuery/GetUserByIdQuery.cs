using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RO.DevTest.Application.Features.User.Queries.GetAllUsersQuery;

namespace RO.DevTest.Application.Features.User.Queries.GetUserByIdQuery
{
    public class GetUserByIdQuery : IRequest<GetUserByIdResult>
    {

        public string Id { get; set; }

        public GetUserByIdQuery(string id)
        {
            Id = id;
        }

    }
}
