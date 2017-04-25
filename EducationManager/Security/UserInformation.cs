using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EducationManager.Models.DataModel;

namespace EducationManager.Security
{
    public class UserInformation
    {
        public int UserId { get; set; }
        public Admin Admin { get; set; }
        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
