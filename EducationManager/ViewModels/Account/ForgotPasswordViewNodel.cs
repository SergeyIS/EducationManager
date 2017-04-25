using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationManager.ViewModels.Account
{
    public class ForgotPasswordViewNodel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}