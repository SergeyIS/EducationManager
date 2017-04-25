using EducationManager.DataModels;
using System;
using System.Collections.Generic;

namespace EducationManager.ViewModels.Student
{
    public class ClassCourseViewModel
    {
        public int ClassId { get; set; }
        public int CourseId { get; set; }
    }
    public class ClassEditorViewModel
    {
        public ClassViewModel AboutClass { get; set; }
        public List<StudentViewModel> Students { get; set; }
    }
    public class ClassViewModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

    }
    public class StudentGeneralViewModel
    {
        public string CourseName { get; set; }
        public List<Grade> Grades { get; set; }
    }
    public class StudentGradesViewModel
    {
        public int? CourseId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public int AddresId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }

    }

}