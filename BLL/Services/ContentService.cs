using BLL.Interface.Entities;
using BLL.Interface.Services;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Mappers;

namespace BLL.Services
{
    public class ContentService : IContentService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalAnswer> answerRepository;
        private readonly IRepository<DalAttachmentContent> contentRepository;
        private readonly IRepository<DalTest> testRepository;

        public ContentService(IUnitOfWork uow)
        {
            this.uow = uow;
            this.answerRepository = uow.AnswerRepository;
            this.contentRepository = uow.ContentRepository;
            this.testRepository = uow.TestRepository;
        }

        public IEnumerable<AnswerEntity> GetAllAnswerEntities()
        {
            return answerRepository.GetAll().Select(answ => answ.ToBllAnswer());
        }

        public void CreateAnswer(AnswerEntity answer)
        {
            answerRepository.Create(answer.ToDalAnswer());
            uow.Commit();
        }

        public IEnumerable<AttachmentContentEntity> GetAllAttachmentContentEntities()
        {
            return contentRepository.GetAll().Select(answ => answ.ToBllAttachmentContent());
        }

        public void CreateAttachmentContent(AttachmentContentEntity content)
        {
            contentRepository.Create(content.ToDalAttachmentContent());
            uow.Commit();
        }

        public IEnumerable<AttachmentContentEntity> GetTestAttachmentContentEntities()
        {
            return contentRepository.GetAll().Where(ent => ent.Answer == null && ent.Blocks.Count() == 0).Select(ent => ent.ToBllAttachmentContent());
        }

        public IEnumerable<AttachmentContentEntity> GetAnswerAttachmentContentEntities()
        {
            return contentRepository.GetAll().Where(ent => ent.Answer != null).Select(ent => ent.ToBllAttachmentContent());
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
