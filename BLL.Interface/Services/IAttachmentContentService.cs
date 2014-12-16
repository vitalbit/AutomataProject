using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IAttachmentContentService
    {
        IEnumerable<AttachmentContentEntity> GetAllAttachmentContentEntities();
        void CreateAttachmentContent(AttachmentContentEntity content);
    }
}
