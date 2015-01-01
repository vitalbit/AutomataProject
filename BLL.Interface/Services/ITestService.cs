using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface ITestService
    {
        IEnumerable<TestEntity> GetAllTestEntities();
        void CreateTest(TestEntity test);
        void SetAttachmentContent(TestEntity test, IEnumerable<AttachmentContentEntity> contents);
        IEnumerable<AttachmentContentEntity> GetAttachmentContents(TestEntity test);
    }
}
