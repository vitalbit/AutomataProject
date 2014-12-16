using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalAnswer : IDalEntity
    {
        public int Id { get; set; }
        public int? TestId { get; set; }
        public int? UserId { get; set; }
        public int? Mark { get; set; }
    }
}
