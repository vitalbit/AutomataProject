using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORM
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public Group()
        {
            Users = new HashSet<User>();
        }
    }
}
