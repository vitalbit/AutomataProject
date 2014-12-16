using BLL.Interface.Services;
using BLL.Interface.Entities;
using BLL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class BlockService : IBlockService
    {
        private readonly IUnitOfWork uow;
        private readonly IBlockRepository blockRepository;

        public BlockService(IUnitOfWork uow, IBlockRepository repository)
        {
            this.uow = uow;
            this.blockRepository = repository;        
        }

        public IEnumerable<BlockEntity> GetAllBlockEntities()
        {
            return blockRepository.GetAll().Select(answ => answ.ToBllBlock());
        }

        public void CreateBlock(BlockEntity block)
        {
            blockRepository.Create(block.ToDalBlock());
            uow.Commit();
        }


        public IEnumerable<BlockEntity> GetHomeBlocksEntities()
        {
            throw new NotImplementedException();
        }
    }
}
