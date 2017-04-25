using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationManager.ViewModels.Admin
{
    public class FindResultViewModel
    {
        public int id { get; set; }
        public string role { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string mname { get; set; }
        public DateTime dateofbirth { get; set; }
    }
}