using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalAttachmentContent : IDalEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public DalAnswer Answer { get; set; }
        public IEnumerable<DalTest> Tests { get; set; }
        public IEnumerable<DalBlock> Blocks { get; set; }
    }
}
