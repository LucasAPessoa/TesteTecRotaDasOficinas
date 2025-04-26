namespace RO.DevTest.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(RO.DevTest.Domain.Entities.User user);
}