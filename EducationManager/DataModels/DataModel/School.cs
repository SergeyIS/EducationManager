using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationManager.Models.DataModel
{
    public class School
    {
        [Key]
        public int SchoolId { get; set; }
        public int AddresId { get; set; }
        public string SchoolName { get; set; }
    }
}
