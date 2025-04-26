
using MediatR;


namespace RO.DevTest.Application.Features.User.Commands.DeleteUserCommand
{
    public class DeleteUserCommand : IRequest<DeleteUserResult>
    {

        public string Id { get; set; }

        public DeleteUserCommand(string id)
        {
            Id = id;
        }

    }
}
