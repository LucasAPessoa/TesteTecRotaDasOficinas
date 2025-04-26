
using Microsoft.AspNetCore.Identity;

namespace RO.DevTest.Application.Features.User.Commands.DeleteUserCommand
{
    public record DeleteUserResult
    {
       public bool Succeeded { get; }

       public IEnumerable<string> Errors { get; }

       public DeleteUserResult(IdentityResult result)
       {
           Succeeded = result.Succeeded;
           Errors = result.Errors.Select(e => e.Description);
       }
    }
}