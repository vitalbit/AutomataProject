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
    public class FacultyRepository : IFacultyRepository
    {
        private readonly DbContext context;

        public FacultyRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
        }

        public IEnumerable<DalFaculty> GetAll()
        {
            return context.Set<Faculty>().ToList().Select(answ => answ.ToDalFaculty());
        }

        public DalFaculty GetById(int key)
        {
            return context.Set<Faculty>().ToList().FirstOrDefault(answ => answ.FacultyId == key).ToDalFaculty();
        }

        public DalFaculty GetByPredicate(System.Linq.Expressions.Expression<Func<DalFaculty, bool>> f)
        {
            Func<DalFaculty, bool> func = f.Compile();
            IEnumerable<DalFaculty> answers = GetAll();
            return answers.FirstOrDefault(answ => func(answ));
        }

        public void Create(DalFaculty e)
        {
            context.Set<Faculty>().Add(e.ToOrmFaculty());
        }

        public void Delete(DalFaculty e)
        {
            context.Set<Faculty>().Remove(e.ToOrmFaculty());
        }

        public void Update(DalFaculty e)
        {
            context.Entry(e.ToOrmFaculty()).State = EntityState.Modified;
        }
    }
}
