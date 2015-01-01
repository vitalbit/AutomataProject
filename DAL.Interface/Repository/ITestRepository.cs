using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Repository
{
    public interface ITestRepository : IRepository<DalTest>
    {
        void SetAttachmentContent(DalTest test, IEnumerable<DalAttachmentContent> contents);
        IEnumerable<DalAttachmentContent> GetAttachmentContents(DalTest test);
    }
}
