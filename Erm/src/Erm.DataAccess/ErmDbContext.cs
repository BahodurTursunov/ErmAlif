using Erm.src.Erm.DataAccess;
using Erm.src.Erm.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Project_ERM.src.Erm.DataAccess
{
    public sealed class ErmDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<BusinessProcess> BusinessProcesses { get; set; } = null!;
        public DbSet<RiskProfile> RiskProfiles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BusinessProcessConfiguration());
            modelBuilder.ApplyConfiguration(new RiskProfileConfiguration());
        }
    }
}