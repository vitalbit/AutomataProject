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
    public class SpecialityRepository : ISpecialityRepository
    {
        private readonly DbContext context;

        public SpecialityRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
        }

        public IEnumerable<DalSpeciality> GetAll()
        {
            return context.Set<Speciality>().ToList().Select(answ => answ.ToDalSpeciality());
        }

        public DalSpeciality GetById(int key)
        {
            return context.Set<Speciality>().ToList().FirstOrDefault(answ => answ.SpecialityId == key).ToDalSpeciality();
        }

        public DalSpeciality GetByPredicate(System.Linq.Expressions.Expression<Func<DalSpeciality, bool>> f)
        {
            Func<DalSpeciality, bool> func = f.Compile();
            IEnumerable<DalSpeciality> answers = GetAll();
            return answers.FirstOrDefault(answ => func(answ));
        }

        public void Create(DalSpeciality e)
        {
            context.Set<Speciality>().Add(e.ToOrmSpeciality());
        }

        public void Delete(DalSpeciality e)
        {
            context.Set<Speciality>().Remove(e.ToOrmSpeciality());
        }

        public void Update(DalSpeciality e)
        {
            context.Entry(e.ToOrmSpeciality()).State = EntityState.Modified;
        }
    }
}
