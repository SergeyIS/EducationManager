using System;
using System.ComponentModel.DataAnnotations;

namespace EducationManager.DataModels
{
    public class UserAccount
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class Address
    {
        [Key]
        public int AddresId { get; set; }
        public string AddresValue { get; set; }
    }
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int SchoolId { get; set; }
        public int UserId { get; set; }
    }
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int SchoolId { get; set; }
    }
    public class ClassCourses
    {
        [Key]
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public string CourseName { get; set; }
        public int SchoolId { get; set; }
    }
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int GradeValue { get; set; }
        public DateTime Date { get; set; }
    }
    public class School
    {
        [Key]
        public int SchoolId { get; set; }
        public int AddresId { get; set; }
        public string SchoolName { get; set; }
    }
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClassId { get; set; }
        public int AddresId { get; set; }
        public int UserId { get; set; }
        public int SchoolId { get; set; }
    }
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int SchoolId { get; set; }
    }
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public int SchoolId { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int AddresId { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    public class OperationRegistryUser
    {
        [Key]
        public int Id { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClassId { get; set; }
        public string Addres { get; set; }
        public int SchoolId { get; set; }
    }
}