using System.Data.Entity;

namespace EducationManager.DataModels
{
    public class DataStorage : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<OperationRegistryUser> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ClassCourses> ClassCourses { get; set; }

        public DataStorage() : base("")
        {

        }
    }
}