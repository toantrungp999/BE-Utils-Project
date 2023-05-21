using Utils.Domain.Entities;
using Utils.Domain.Repositories;
using Utils.Persistence.Contexts;

namespace Utils.Persistence.Repositories
{
    public class TestRepository : Repository<Test, Guid>, ITestRepository
    {
        public TestRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void GetTestValue()
        {
            throw new NotImplementedException();
        }
    }
}
