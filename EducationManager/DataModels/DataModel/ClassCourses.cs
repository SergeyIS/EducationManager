using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationManager.Models.DataModel
{
    public class ClassCourses
    {
        [Key]
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
}
