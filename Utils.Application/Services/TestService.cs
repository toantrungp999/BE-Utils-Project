using Utils.Application.Services.Interfaces;
using Utils.Domain.Entities;
using Utils.Domain.Repositories;

namespace Utils.Application.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _uow;
        public TestService(ITestRepository testRepository, ICategoryRepository categoryRepository, IUnitOfWork uow)
        {
            _testRepository = testRepository;
            _categoryRepository = categoryRepository;
            _uow = uow;
        }
        public async Task<string> AddTest()
        {
            var test = new Test
            {
                TestValue = "Test Value",
            };

            var category = new Category
            {
                Name = "Category Name",
            };

            await _testRepository.AddAsync(test);
            await _categoryRepository.AddAsync(category);
            await _uow.SaveChangesAsync();
            var id = test.Id.ToString();
            return id;
        }

        public async Task<string> AddTest(Guid id)
        {
            var test = new Test
            {
                Id = id,
                TestValue = "Add Test in background",
            };

            await _testRepository.AddAsync(test);
            await _uow.SaveChangesAsync();

            return id.ToString();
        }

        public List<Test> GetAllTest()
        {
            var tests = _testRepository.GetAll().ToList();
            return tests;
        }

        public Test GetFirstTest()
        {
            var test = _testRepository.GetAll().FirstOrDefault();
            return test;
        }

        public string UpdateFirstTest()
        {
            var test = _testRepository.GetAll().FirstOrDefault();
            test.TestValue = "Update new value";
            _uow.SaveChanges();
            return test.TestValue;
        }
    }
}
