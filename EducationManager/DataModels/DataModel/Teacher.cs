using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationManager.Models.DataModel
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public int SchoolId { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int AddresId { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
