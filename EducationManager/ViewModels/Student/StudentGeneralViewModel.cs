using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EducationManager.Models.DataModel;

namespace EducationManager.ViewModels.Student
{
    public class StudentGeneralViewModel
    {
        public string CourseName { get; set; }
        public List<Grade> Grades { get; set; }
    }
}