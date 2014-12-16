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
    public class OptionRepository : IOptionRepository
    {
        private readonly DbContext context;

        public OptionRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
        }

        public IEnumerable<DalOption> GetAll()
        {
            return context.Set<Option>().ToList().Select(answ => answ.ToDalOption());
        }

        public DalOption GetById(int key)
        {
            return context.Set<Option>().ToList().FirstOrDefault(answ => answ.OptionId == key).ToDalOption();
        }

        public DalOption GetByPredicate(System.Linq.Expressions.Expression<Func<DalOption, bool>> f)
        {
            Func<DalOption, bool> func = f.Compile();
            IEnumerable<DalOption> answers = GetAll();
            return answers.FirstOrDefault(answ => func(answ));
        }

        public void Create(DalOption e)
        {
            context.Set<Option>().Add(e.ToOrmOption());
        }

        public void Delete(DalOption e)
        {
            context.Set<Option>().Remove(e.ToOrmOption());
        }

        public void Update(DalOption e)
        {
            context.Entry(e.ToOrmOption()).State = EntityState.Modified;
        }
    }
}
