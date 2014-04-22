using Microsoft.AspNet.Identity.EntityFramework;

namespace igoryen2.Models {
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser {
        public string HomeTown { get; set; }
        public virtual MyUserInfo MyUserInfo { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext()
            : base("DefaultConnection") {
        }
    }
}