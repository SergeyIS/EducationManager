using System;

namespace EducationManager.ViewModels.Admin
{
    public class BidViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Addres { get; set; }
        public int ClassId { get; set; }
        public int SchoolId { get; set; }
        public bool IsConfirmed { get; set; }

    }
    public class DeleteUserViewModel
    {
        public string Role { get; set; }
        public int _ID { get; set; }
    }
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