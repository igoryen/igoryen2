using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;


namespace igoryen2.Models {
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser {
        public string HomeTown { get; set; }
        public virtual MyUserInfo MyUserInfo { get; set; }
    }

    public class DataContext : IdentityDbContext<ApplicationUser> {
        public DataContext()
            : base("DefaultConnection") {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
        }

        public DbSet<Cancellation> Cancellations { get; set; }
        public DbSet<ComMethod> ComMethods { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        //public DbSet<Message> Messages { get; set; }
        public DbSet<MyUserInfo> MyUserInfo { get; set; }
        public DbSet<Student> Students { get; set; }

        public System.Data.Entity.DbSet<igoryen2.ViewModels.CancellationCreateForHttpGet> CancellationCreateForHttpGets { get; set; }

        public System.Data.Entity.DbSet<igoryen2.ViewModels.CourseCreateForHttpGet> CourseCreateForHttpGets { get; set; }
    }
}