using Utils.Domain.Entities;

namespace Utils.Application.Services.Interfaces
{
    public interface ITestService
    {
        Task<string> AddTest();
        Task<string> AddTest(Guid id);
        Test GetFirstTest();
        List<Test> GetAllTest();
        string UpdateFirstTest();
    }
}
