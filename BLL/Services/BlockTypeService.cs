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
    public class BlockTypeService : IBlockTypeService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalBlockType> blockTypeRepository;

        public BlockTypeService(IUnitOfWork uow)
        {
            this.uow = uow;
            this.blockTypeRepository = uow.BlockTypeRepository;
        }

        public IEnumerable<BlockTypeEntity> GetAllBlockTypeEntities()
        {
            return blockTypeRepository.GetAll().Select(answ => answ.ToBllBlockType());
        }

        public void CreateBlockType(BlockTypeEntity blockType)
        {
            blockTypeRepository.Create(blockType.ToDalBlockType());
            uow.Commit();
        }

        public void Dispose()
        {
            uow.Dispose();
        }
    }
}
