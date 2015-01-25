using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IBlockService : IDisposable
    {
        IEnumerable<BlockEntity> GetAllBlockEntities();
        IEnumerable<BlockEntity> GetHomeBlocksEntities();
        void CreateBlock(BlockEntity block);
        IEnumerable<BlockTypeEntity> GetAllBlockTypeEntities();
        void CreateBlockType(BlockTypeEntity blockType);
    }
}
