using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RO.DevTest.Domain.Entities;
using UserEntity = RO.DevTest.Domain.Entities.User;

namespace RO.DevTest.Application.Features.User.Queries.GetUserByIdQuery
{
    public class GetUserByIdResult
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public GetUserByIdResult(UserEntity user)
        {
            Id = user.Id;
            Name = user.Name!;
            Email = user.Email!;
            PhoneNumber = user.PhoneNumber!;
            UserName = user.UserName!;
        }
    }
}
