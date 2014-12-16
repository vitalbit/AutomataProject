using BLL.Interface.Services;
using BLL.Interface.Entities;
using BLL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork uow;
        private readonly ITestRepository testRepository;

        public TestService(IUnitOfWork uow, ITestRepository repository)
        {
            this.uow = uow;
            this.testRepository = repository;
        }

        public IEnumerable<TestEntity> GetAllTestEntities()
        {
            return testRepository.GetAll().Select(answ => answ.ToBllTest());
        }

        public void CreateTest(TestEntity test)
        {
            testRepository.Create(test.ToDalTest());
            uow.Commit();
        }
    }
}
