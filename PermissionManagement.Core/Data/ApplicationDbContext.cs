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

            builder.Entity<Page>().HasData(new Page { Id = 1, Name = "Page1", AssociatedRole = Constants.Roles.Member.ToString() });
            builder.Entity<Page>().HasData(new Page { Id = 2, Name = "Page2", AssociatedRole = Constants.Roles.Member.ToString() });
            builder.Entity<Page>().HasData(new Page { Id = 3, Name = "PermissionsManagement", AssociatedRole = Constants.Roles.Administrator.ToString() });
            builder.Entity<Block>().HasData(new Block { Id = 1, Name = "Block1", PageId = 1 });
            builder.Entity<Block>().HasData(new Block { Id = 2, Name = "Block2", PageId = 1 });
            builder.Entity<Block>().HasData(new Block { Id = 3, Name = "Block3", PageId = 1 });
            builder.Entity<Block>().HasData(new Block { Id = 4, Name = "Block4", PageId = 2 });
            builder.Entity<Block>().HasData(new Block { Id = 5, Name = "Block5", PageId = 2 });
        }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Block> Blocks { get; set; }
    }
}