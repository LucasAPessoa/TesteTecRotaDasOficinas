using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.User.Commands.UpdateUserCommand
{


    public class UpdateUserResult
    {
        public string Id { get;  }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public UpdateUserResult(Domain.Entities.User user)
        {
            if (string.IsNullOrWhiteSpace(user.Id))
                throw new ArgumentNullException(nameof(user.Id), "Id é obrigatório");

            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            Name = user.Name;
            PhoneNumber = user.PhoneNumber;
        }
    }
}
