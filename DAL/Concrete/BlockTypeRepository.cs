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
    public class BlockTypeRepository : IBlockTypeRepository
    {
        private readonly DbContext context;

        public BlockTypeRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
        }

        public IEnumerable<DalBlockType> GetAll()
        {
            return context.Set<BlockType>().ToList().Select(answ => answ.ToDalBlockType());
        }

        public DalBlockType GetById(int key)
        {
            return context.Set<BlockType>().ToList().FirstOrDefault(answ => answ.BlockTypeId == key).ToDalBlockType();
        }

        public DalBlockType GetByPredicate(System.Linq.Expressions.Expression<Func<DalBlockType, bool>> f)
        {
            Func<DalBlockType, bool> func = f.Compile();
            IEnumerable<DalBlockType> answers = GetAll();
            return answers.FirstOrDefault(answ => func(answ));
        }

        public void Create(DalBlockType e)
        {
            context.Set<BlockType>().Add(e.ToOrmBlockType());
        }

        public void Delete(DalBlockType e)
        {
            context.Set<BlockType>().Remove(e.ToOrmBlockType());
        }

        public void Update(DalBlockType e)
        {
            context.Entry(e.ToOrmBlockType()).State = EntityState.Modified;
        }
    }
}
