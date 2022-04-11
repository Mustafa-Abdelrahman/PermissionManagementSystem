using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.Web.Models;
using PermissionManagement.Web.ViewModels;

namespace PermissionManagement.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");

            builder.Entity<IdentityUser>().Ignore(c => c.AccessFailedCount)
                                           .Ignore(c => c.LockoutEnabled)
                                           .Ignore(c => c.PhoneNumberConfirmed)
                                           .Ignore(c => c.TwoFactorEnabled);
        }

        public DbSet<PermissionManagement.Web.ViewModels.EditPermissionsVM> EditPermissionsVM { get; set; }
    }
}