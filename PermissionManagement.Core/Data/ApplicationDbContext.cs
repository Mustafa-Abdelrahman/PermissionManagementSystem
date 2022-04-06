using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.Web.Models;

namespace PermissionManagement.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Page> Pages { get; set; }
        public DbSet<Block> Blocks { get; set; }
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

            builder.Entity<Page>().HasData(new Page { Id = 1, Name = "Page 1" });
            builder.Entity<Page>().HasData(new Page { Id = 2, Name = "Page 2" });
            builder.Entity<Page>().HasData(new Page { Id = 3, Name = "PermissionsManagement" });
            builder.Entity<Block>().HasData(new Block { Id = 1, Name = "Block 1", PageId = 1 });
            builder.Entity<Block>().HasData(new Block { Id = 2, Name = "Block 2", PageId = 1 });
            builder.Entity<Block>().HasData(new Block { Id = 3, Name = "Block 3", PageId = 1 });
            builder.Entity<Block>().HasData(new Block { Id = 4, Name = "Block 4", PageId = 2 });
            builder.Entity<Block>().HasData(new Block { Id = 5, Name = "Block 5", PageId = 2 });
        }
    }
}