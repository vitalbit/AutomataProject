using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IContentService : IDisposable
    {
        IEnumerable<AnswerEntity> GetAllAnswerEntities();
        void CreateAnswer(AnswerEntity answer);
        IEnumerable<AttachmentContentEntity> GetAllAttachmentContentEntities();
        void CreateAttachmentContent(AttachmentContentEntity content);
        IEnumerable<AttachmentContentEntity> GetTestAttachmentContentEntities();
        IEnumerable<AttachmentContentEntity> GetAnswerAttachmentContentEntities();
        IEnumerable<TestEntity> GetAllTestEntities();
        void CreateTest(TestEntity test);
        void SetAttachmentContent(TestEntity test, IEnumerable<AttachmentContentEntity> contents);
        IEnumerable<AttachmentContentEntity> GetAttachmentContents(TestEntity test);
    }
}
