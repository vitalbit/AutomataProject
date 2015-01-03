using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAutomation.Models
{
    public class ListAnswersViewModel
    {
        public int AttachmentContentId { get; set; }
        public int? TestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Faculty { get; set; }
        public int? Course { get; set; }
        public string Group { get; set; }
        public string Speciality { get; set; }
    }
}