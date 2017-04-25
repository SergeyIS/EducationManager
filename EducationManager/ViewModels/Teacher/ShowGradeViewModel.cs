using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationManager.ViewModels.Teacher
{
    public class ShowGradeViewModel
    {
        public int CourseId { get; set; }
        public int ClassId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}