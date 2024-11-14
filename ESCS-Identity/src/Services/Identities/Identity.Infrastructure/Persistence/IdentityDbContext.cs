using Core.Infrastructure.Persistence;
using Identity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Identity.Infrastructure.Persistence
{
    public class IdentityDbContext : BaseDbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<EmailServiceConfig> EmailServiceConfigs { get; set; }

        public IdentityDbContext(DbContextOptions opt) : base(opt) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // TODO: Check
            base.OnModelCreating(modelBuilder);
        }
    }
}
