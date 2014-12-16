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
    public class GroupRepository : IGroupRepository
    {
        private readonly DbContext context;

        public GroupRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
        }

        public IEnumerable<DalGroup> GetAll()
        {
            return context.Set<Group>().ToList().Select(answ => answ.ToDalGroup());
        }

        public DalGroup GetById(int key)
        {
            return context.Set<Group>().ToList().FirstOrDefault(answ => answ.GroupId == key).ToDalGroup();
        }

        public DalGroup GetByPredicate(System.Linq.Expressions.Expression<Func<DalGroup, bool>> f)
        {
            Func<DalGroup, bool> func = f.Compile();
            IEnumerable<DalGroup> answers = GetAll();
            return answers.FirstOrDefault(answ => func(answ));
        }

        public void Create(DalGroup e)
        {
            context.Set<Group>().Add(e.ToOrmGroup());
        }

        public void Delete(DalGroup e)
        {
            context.Set<Group>().Remove(e.ToOrmGroup());
        }

        public void Update(DalGroup e)
        {
            context.Entry(e.ToOrmGroup()).State = EntityState.Modified;
        }
    }
}
