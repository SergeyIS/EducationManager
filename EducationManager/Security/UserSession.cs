using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationManager.Security
{
    public static class UserSession
    {
        static string userDataSessionvar = "UserData";

        public static UserInformation Uinform
        {
            get
            {
                if (HttpContext.Current == null)
                    return null;
                var sessionVar = HttpContext.Current.Session[userDataSessionvar];
                return sessionVar as UserInformation;
            }
            set
            {
                HttpContext.Current.Session[userDataSessionvar] = value;
            }
        }
    }
}
