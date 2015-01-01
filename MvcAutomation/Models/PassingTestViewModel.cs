using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAutomation.Models
{
    public class PassingTestViewModel
    {
        public int TestNum { get; set; }
        public string TestName { get; set; }
        public string Regex { get; set; }
        public string[] GraphArray { get; set; }
        public int States { get; set; }
        public int Values { get; set; }
        public int?[] FinalStates { get; set; }
        public string[] ValuesArray { get; set; }
        public int? TestTime { get; set; }
        public int? TestCount { get; set; }
    }
}