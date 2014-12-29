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
    public class AttachmentContentService : IAttachmentContentService
    {
        private readonly IUnitOfWork uow;
        private readonly IAttachmentContentRepository contentRepository;

        public AttachmentContentService(IUnitOfWork uow, IAttachmentContentRepository repository)
        {
            this.uow = uow;
            this.contentRepository = repository;        
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
            return contentRepository.GetAllTestFiles().Select(ent => ent.ToBllAttachmentContent());
        }

        public IEnumerable<AttachmentContentEntity> GetAnswerAttachmentContentEntities()
        {
            return contentRepository.GetAllAnswerFiles().Select(ent => ent.ToBllAttachmentContent());
        }
    }
}
