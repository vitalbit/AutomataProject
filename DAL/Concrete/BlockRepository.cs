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
    public class BlockRepository : IBlockRepository
    {
        private readonly DbContext context;

        public BlockRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
        }

        public IEnumerable<DalBlock> GetAll()
        {
            return context.Set<Block>().ToList().Select(answ => answ.ToDalBlock());
        }

        public DalBlock GetById(int key)
        {
            return context.Set<Block>().ToList().FirstOrDefault(answ => answ.BlockId == key).ToDalBlock();
        }

        public DalBlock GetByPredicate(System.Linq.Expressions.Expression<Func<DalBlock, bool>> f)
        {
            Func<DalBlock, bool> func = f.Compile();
            IEnumerable<DalBlock> answers = GetAll();
            return answers.FirstOrDefault(answ => func(answ));
        }

        public void Create(DalBlock e)
        {
            context.Set<Block>().Add(e.ToOrmBlock());
        }

        public void Delete(DalBlock e)
        {
            context.Set<Block>().Remove(e.ToOrmBlock());
        }

        public void Update(DalBlock e)
        {
            context.Entry(e.ToOrmBlock()).State = EntityState.Modified;
        }
    }
}
