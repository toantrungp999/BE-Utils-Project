using Utils.Domain.Entities;
using Utils.Domain.Repositories;
using Utils.Persistence.Contexts;

namespace Utils.Persistence.Repositories
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<string> AddUser()
        {
            throw new NotImplementedException();
        }
    }
}
