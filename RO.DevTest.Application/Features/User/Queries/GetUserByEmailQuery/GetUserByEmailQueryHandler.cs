using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Application.Contracts.Persistance.Repositories;

namespace RO.DevTest.Application.Features.User.Queries.GetUserByEmailQuery
{
    public class GetUserByEmailQueryHandler(IIdentityAbstractor identityAbstractor) : IRequestHandler<GetUserByEmailQuery, GetUserByEmailResult>
    {
        private readonly IIdentityAbstractor _identityAbstractor = identityAbstractor;

        public async Task<GetUserByEmailResult> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _identityAbstractor.FindUserByEmailAsync(request.Email) ?? throw new ArgumentException("Usuário não encontrado");

            var result = new GetUserByEmailResult(user);
            return result;
        }

      
    }
}
