using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalTest : IDalEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TestCount { get; set; }
        public int? TestTime { get; set; }
        public ICollection<DalAnswer> Answers { get; set; }
        public ICollection<DalAttachmentContent> AttachmentContents { get; set; }
    }
}
