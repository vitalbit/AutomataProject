using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ORM
{
    public class Test
    {
        public int TestId { get; set; }
        public string Name { get; set; }
        public int? TestCount { get; set; }
        public int? TestTime { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<AttachmentContent> AttachmentContents { get; set; }
        public Test()
        {
            AttachmentContents = new HashSet<AttachmentContent>();
            Answers = new HashSet<Answer>();
        }
    }
}
