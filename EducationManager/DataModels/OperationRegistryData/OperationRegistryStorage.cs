using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EducationManager.Models.OperationRegistryData
{
    public class OperationRegistryStorage : DbContext
    {
        public DbSet<OperationRegistryUser> Users { get; set; }
    }
}
