using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORM
{
    public class AttachmentContent : IORMEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public virtual Answer Answer { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<Block> Blocks { get; set; }
        public AttachmentContent()
        {
            Tests = new HashSet<Test>();
            Blocks = new HashSet<Block>();
        }
    }
}
