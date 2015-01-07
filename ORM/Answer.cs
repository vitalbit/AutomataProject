using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ORM
{
    public class Answer
    {
        [Key, ForeignKey("AttachmentContent")]
        public int AttachmentContentId { get; set; }
        public int? TestId { get; set; }
        public int? UserId { get; set; }
        public double? Mark { get; set; }
        public virtual AttachmentContent AttachmentContent { get; set; }
        public virtual Test Test { get; set; }
        public virtual User User { get; set; }
    }
}
