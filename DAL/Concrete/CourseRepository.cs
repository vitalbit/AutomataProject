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
    public class CourseRepository : ICourseRepository
    {
        private readonly DbContext context;

        public CourseRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
        }

        public IEnumerable<DalCourse> GetAll()
        {
            return context.Set<Course>().ToList().Select(answ => answ.ToDalCourse());
        }

        public DalCourse GetById(int key)
        {
            return context.Set<Course>().ToList().FirstOrDefault(answ => answ.CourseId == key).ToDalCourse();
        }

        public DalCourse GetByPredicate(System.Linq.Expressions.Expression<Func<DalCourse, bool>> f)
        {
            Func<DalCourse, bool> func = f.Compile();
            IEnumerable<DalCourse> answers = GetAll();
            return answers.FirstOrDefault(answ => func(answ));
        }

        public void Create(DalCourse e)
        {
            context.Set<Course>().Add(e.ToOrmCourse());
        }

        public void Delete(DalCourse e)
        {
            context.Set<Course>().Remove(e.ToOrmCourse());
        }

        public void Update(DalCourse e)
        {
            context.Entry(e.ToOrmCourse()).State = EntityState.Modified;
        }
    }
}
