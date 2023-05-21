using Utils.Domain.Entities;
using Utils.Domain.Repositories;
using Utils.Persistence.Contexts;

namespace Utils.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<string> AddCategory()
        {
            throw new NotImplementedException();
        }
    }
}
