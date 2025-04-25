using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Persistence.Repositories;

public class UserRepository: 
    BaseRepository<User>, IUserRepository {

    private readonly DefaultContext _context;

    public UserRepository(DefaultContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<User> GetAllAsQueryable()
    {
        return _context.Users.AsQueryable();
    }

 

}
