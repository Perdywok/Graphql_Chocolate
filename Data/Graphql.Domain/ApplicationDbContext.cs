using Graphql.Domain.Entities;
using GraphqlDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphqlDomain
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Contact> Contacts { get; set; } = default!;
        public DbSet<Role> Roles { get; set; } = default!;
        public DbSet<UserRole> UserRoles { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            // Many-to-many: User <-> Role
            modelBuilder
                .Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
        }
    }
}
