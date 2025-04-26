using UserEntity = RO.DevTest.Domain.Entities.User;

namespace RO.DevTest.Application.Features.User.Queries.GetUserByEmailQuery
{
    public class GetUserByEmailResult
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public GetUserByEmailResult(UserEntity user)
        {
            Id = user.Id;
            Name = user.Name!;
            Email = user.Email!;
            PhoneNumber = user.PhoneNumber!;
            UserName = user.UserName!;
        }
    }
}
