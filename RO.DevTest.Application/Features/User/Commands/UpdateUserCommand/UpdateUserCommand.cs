
using MediatR;

namespace RO.DevTest.Application.Features.User.Commands.UpdateUserCommand
{
    public class UpdateUserCommand : IRequest<UpdateUserResult>
    {
        public string Id { get; init; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public UpdateUserCommand(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id), "Id é obrigatório");
        }
    }
}
