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
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext context;

        public RoleRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
        }

        public IEnumerable<DalRole> GetAll()
        {
            return context.Set<Role>().ToList().Select(answ => answ.ToDalRole());
        }

        public DalRole GetById(int key)
        {
            return context.Set<Role>().ToList().FirstOrDefault(answ => answ.RoleId == key).ToDalRole();
        }

        public DalRole GetByPredicate(System.Linq.Expressions.Expression<Func<DalRole, bool>> f)
        {
            Func<DalRole, bool> func = f.Compile();
            IEnumerable<DalRole> answers = GetAll();
            return answers.FirstOrDefault(answ => func(answ));
        }

        public void Create(DalRole e)
        {
            context.Set<Role>().Add(e.ToOrmRole());
        }

        public void Delete(DalRole e)
        {
            context.Set<Role>().Remove(e.ToOrmRole());
        }

        public void Update(DalRole e)
        {
            context.Entry(e.ToOrmRole()).State = EntityState.Modified;
        }
    }
}
