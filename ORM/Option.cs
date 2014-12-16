using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class Option
    {
        public int OptionId { get; set; }
        public int? TestCount { get; set; }
        public int? TestTime { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        public Option()
        {
            Tests = new HashSet<Test>();
        }
    }
}
