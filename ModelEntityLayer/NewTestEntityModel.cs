using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEntityLayer
{
    public class NewTestEntityModel
    {
        public string TestName { get; set; }
        public string Description { get; set; }
        public string Regex { get; set; }
        public string[] GraphArray { get; set; }
        public int States { get; set; }
        public int Values { get; set; }
        public int?[] FinalStates { get; set; }
        public string[] ValuesArray { get; set; }
    }
}
