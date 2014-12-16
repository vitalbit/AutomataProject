using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
using ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class AttachmentContentRepository : IAttachmentContentRepository
    {
        private readonly DbContext context;

        public AttachmentContentRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
        }

        public IEnumerable<DalAttachmentContent> GetAll()
        {
            return context.Set<AttachmentContent>().ToList().Select(answ => answ.ToDalAttachmentContent());
        }

        public DalAttachmentContent GetById(int key)
        {
            return context.Set<AttachmentContent>().ToList().FirstOrDefault(answ => answ.AttachmentContentId == key).ToDalAttachmentContent();
        }

        public DalAttachmentContent GetByPredicate(System.Linq.Expressions.Expression<Func<DalAttachmentContent, bool>> f)
        {
            Func<DalAttachmentContent, bool> func = f.Compile();
            IEnumerable<DalAttachmentContent> answers = GetAll();
            return answers.FirstOrDefault(answ => func(answ));
        }

        public void Create(DalAttachmentContent e)
        {
            context.Set<AttachmentContent>().Add(e.ToOrmAttachmentContent());
        }

        public void Delete(DalAttachmentContent e)
        {
            context.Set<AttachmentContent>().Remove(e.ToOrmAttachmentContent());
        }

        public void Update(DalAttachmentContent e)
        {
            context.Entry(e.ToOrmAttachmentContent()).State = EntityState.Modified;
        }
    }
}
