using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using RO.DevTest.Application.Contracts.Infrastructure;

namespace RO.DevTest.Application.Features.User.Queries.GetUserByIdQuery
{
    public class GetUserByIdQueryHandler(IIdentityAbstractor identityAbstractor) : IRequestHandler<GetUserByIdQuery, GetUserByIdResult>
    {
        private readonly IIdentityAbstractor _identityAbstractor = identityAbstractor;

        public async Task<GetUserByIdResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _identityAbstractor.FindUserByIdAsync(request.Id) ?? throw new ArgumentException("Usuário não encontrado");

            var result = new GetUserByIdResult(user);
            return result;
        }

    }
}
