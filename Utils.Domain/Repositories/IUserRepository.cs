using Utils.Domain.Entities;

namespace Utils.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
        Task<string> AddCategory();
    }
}
