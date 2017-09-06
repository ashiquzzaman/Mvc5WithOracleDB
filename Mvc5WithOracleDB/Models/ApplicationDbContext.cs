using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Mvc5WithOracleDB.Models
{
    //  [DbConfigurationType("OSAD_Base.EfDbConfiguration, OSAD_Base")]
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("OracleDbContext", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("C##AML");
            modelBuilder
                .Properties()
                .Where(p => p.PropertyType == typeof(string) &&
                            !p.Name.Contains("Id") &&
                            !p.Name.Contains("Provider"))
                .Configure(p => p.HasMaxLength(256));

            modelBuilder.Entity<IdentityRole>().ToTable("Roles").Property(c => c.Name).HasMaxLength(128).IsRequired();
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(c => c.UserName).HasMaxLength(128).IsRequired();
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(c => c.Email).HasMaxLength(128).IsRequired();
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}