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
    public class TestRepository : ITestRepository
    {
        private readonly DbContext context;

        public TestRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
        }

        public IEnumerable<DalTest> GetAll()
        {
            return context.Set<Test>().ToList().Select(answ => answ.ToDalTest());
        }

        public DalTest GetById(int key)
        {
            return context.Set<Test>().ToList().FirstOrDefault(answ => answ.TestId == key).ToDalTest();
        }

        public DalTest GetByPredicate(System.Linq.Expressions.Expression<Func<DalTest, bool>> f)
        {
            Func<DalTest, bool> func = f.Compile();
            IEnumerable<DalTest> answers = GetAll();
            return answers.FirstOrDefault(answ => func(answ));
        }

        public void Create(DalTest e)
        {
            context.Set<Test>().Add(e.ToOrmTest());
        }

        public void Delete(DalTest e)
        {
            context.Set<Test>().Remove(e.ToOrmTest());
        }

        public void Update(DalTest e)
        {
            context.Entry(e.ToOrmTest()).State = EntityState.Modified;
        }
    }
}
