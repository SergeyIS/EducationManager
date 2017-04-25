using System;

namespace EducationManager.ViewModels.Account
{
    public class AccountViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class ForgotPasswordViewNodel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
    public class RegistryViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Addres { get; set; }
        public int ClassId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int SchoolId { get; set; }
    }

}