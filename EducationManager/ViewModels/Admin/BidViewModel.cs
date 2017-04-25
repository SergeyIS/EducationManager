using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationManager.ViewModels.Admin
{
    public class BidViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Addres { get; set; }
        public int ClassId { get; set; }
        public int SchoolId { get; set; }
        public bool IsConfirmed { get; set; }

    }
}