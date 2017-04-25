using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using EducationManager.DataModels;

namespace EducationManager.Security
{
    public class CustomPrincipal : IPrincipal
    {
        private UserAccount Account;

        public CustomPrincipal(UserAccount account)
        {
            this.Account = account;
            this.Identity = new GenericIdentity(account.UserId.ToString());
        }
        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            var roles = role.Split(',');
            return roles.Any(r => this.Account.Role.Equals(r));
        }
    }
}
