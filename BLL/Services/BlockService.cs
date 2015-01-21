using BLL.Interface.Services;
using BLL.Interface.Entities;
using BLL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Repository;
using DAL.Interface.DTO;

namespace BLL.Services
{
    public class BlockService : IBlockService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalBlock> blockRepository;

        public BlockService(IUnitOfWork uow)
        {
            this.uow = uow;
            this.blockRepository = uow.BlockRepository;
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

        public void Dispose()
        {
            uow.Dispose();
        }
    }
}
