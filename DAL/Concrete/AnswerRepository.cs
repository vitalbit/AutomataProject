using DAL.Interface.Repository;
using DAL.Interface.DTO;
using DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace DAL.Concrete
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly DbContext context;

        public AnswerRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
        }

        public IEnumerable<DalAnswer> GetAll()
        {
            return context.Set<Answer>().ToList().Select(answ => answ.ToDalAnswer());
        }

        public DalAnswer GetById(int key)
        {
            return context.Set<Answer>().ToList().FirstOrDefault(answ => answ.AttachmentContentId == key).ToDalAnswer();
        }

        public DalAnswer GetByPredicate(System.Linq.Expressions.Expression<Func<DalAnswer, bool>> f)
        {
            Func<DalAnswer, bool> func = f.Compile();
            IEnumerable<DalAnswer> answers = GetAll();
            return answers.FirstOrDefault(answ => func(answ));
        }

        public void Create(DalAnswer e)
        {
            context.Set<Answer>().Add(e.ToOrmAnswer());
        }

        public void Delete(DalAnswer e)
        {
            context.Set<Answer>().Remove(e.ToOrmAnswer());
        }

        public void Update(DalAnswer e)
        {
            context.Entry(e.ToOrmAnswer()).State = EntityState.Modified;
        }
    }
}
