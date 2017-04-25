using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationManager.ViewModels.Student
{
    public class ClassViewModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public DateTime DateFrom {get;set;}
        public DateTime DateTo { get; set; }

    }
}