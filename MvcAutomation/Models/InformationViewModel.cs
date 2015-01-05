using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcAutomation.Models
{
    public class InformationViewModel
    {
        [Display(Name = "Номер курса")]
        public string Course { get; set; }
        [Display(Name = "Номер группы")]
        public string Group { get; set; }
        [Display(Name = "Специальность")]
        public string Speciality { get; set; }
        [Display(Name = "Факультет")]
        public string Faculty { get; set; }
    }
}