﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class BlockType : IORMEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Block> Blocks { get; set; }
        public BlockType()
        {
            Blocks = new HashSet<Block>();
        }
    }
}
