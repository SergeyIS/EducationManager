using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationManager.ViewModels.Student
{
    public class StudentGradesViewModel
    {
        public int? CourseId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}