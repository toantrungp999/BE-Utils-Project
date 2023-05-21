using Utils.Domain.Entities;

namespace Utils.Domain.Repositories
{
    public interface ITestRepository : IRepository<Test, Guid>
    {
        void GetTestValue();
    }
}
