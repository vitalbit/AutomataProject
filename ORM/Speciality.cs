using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class Speciality
    {
        public int SpecialityId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public Speciality()
        {
            Users = new HashSet<User>();
        }
    }
}
