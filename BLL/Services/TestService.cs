using BLL.Interface.Services;
using BLL.Interface.Entities;
using BLL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Repository;
using DAL.Interface.DTO;

namespace BLL.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalTest> testRepository;

        public TestService(IUnitOfWork uow)
        {
            this.uow = uow;
            this.testRepository = uow.TestRepository;
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


        public void SetAttachmentContent(TestEntity test, IEnumerable<AttachmentContentEntity> contents)
        {
            DalTest dalTest = testRepository.GetById(test.Id);
            foreach (var content in contents)
                dalTest.AttachmentContents.Add(content.ToDalAttachmentContent());
            testRepository.Update(dalTest);
            uow.Commit();
        }


        public IEnumerable<AttachmentContentEntity> GetAttachmentContents(TestEntity test)
        {
            DalTest dalTest = testRepository.GetById(test.Id);
            return dalTest.AttachmentContents.Select(ent => ent.ToBllAttachmentContent());
        }

        public void Dispose()
        {
            uow.Dispose();
        }
    }
}
