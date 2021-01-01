using GraphqlDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphqlDomain
{
    class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;
    }
}
