﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class AttachmentContentEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}
