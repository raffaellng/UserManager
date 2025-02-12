using Microsoft.EntityFrameworkCore;
using UserManager.Domain.Entities;
using UserManager.Infrastructure.EntityConfiguration;

namespace UserManager.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MemberConfiguration());
        }

    }
}
