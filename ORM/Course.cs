using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class Course
    {
        public int CourseId { get; set; }
        public int? Number { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public Course()
        {
            Users = new HashSet<User>();
        }
    }
}
