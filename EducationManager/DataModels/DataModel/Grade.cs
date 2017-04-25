using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationManager.Models.DataModel
{
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int GradeValue { get; set; }
        public DateTime Date { get; set; }
    }
}
