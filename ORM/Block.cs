using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class Block : IORMEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int? BlockTypeId { get; set; }
        public virtual ICollection<AttachmentContent> AttachmentContents { get; set; }
        public virtual BlockType BlockType { get; set; }
        public Block()
        {
            AttachmentContents = new HashSet<AttachmentContent>();
        }
    }
}
