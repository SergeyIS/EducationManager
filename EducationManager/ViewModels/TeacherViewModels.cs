using System;
using System.Collections.Generic;
using EducationManager.DataModels;

namespace EducationManager.ViewModels.Teacher
{
    public class GradesViewModel
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int GradeValue { get; set; }

    }
    public class ShowGradeViewModel
    {
        public int CourseId { get; set; }
        public int ClassId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

    }
    public class TeacherGeneralViewModel
    {
        public string FullName { get; set; }
        public List<Grade> Grades { get; set; }

    }

}