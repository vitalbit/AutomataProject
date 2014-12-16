﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalUser : IDalEntity
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? CourseId { get; set; }
        public int? GroupId { get; set; }
        public int? SpecialityId { get; set; }
        public int? FacultyId { get; set; }
        public int? RoleId { get; set; }
    }
}
