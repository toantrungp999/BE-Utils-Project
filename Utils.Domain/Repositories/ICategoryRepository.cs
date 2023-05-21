using Utils.Domain.Entities;

namespace Utils.Domain.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<string> AddUser();
    }
}
