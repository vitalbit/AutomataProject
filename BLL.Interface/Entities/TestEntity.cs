﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class TestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TestCount { get; set; }
        public int? TestTime { get; set; }
    }
}
