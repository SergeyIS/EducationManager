using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EducationManager.Models.AccountModel
{
    public class UserAccountStorage : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}
