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
    public class AttachmentContentService : IAttachmentContentService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalAttachmentContent> contentRepository;

        public AttachmentContentService(IUnitOfWork uow)
        {
            this.uow = uow;
            this.contentRepository = uow.ContentRepository;        
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

        public void Dispose()
        {
            uow.Dispose();
        }
    }
}
