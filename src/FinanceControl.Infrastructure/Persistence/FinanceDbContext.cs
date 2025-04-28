using FinanceControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceControl.Infrastructure.Persistence
{
    public class FinanceDbContext : DbContext
    {
        public DbSet<FixedIncome> FixedIncomes => Set<FixedIncome>();

        public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FixedIncome>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Description).HasMaxLength(100).IsRequired();
                builder.Property(x => x.Amount).IsRequired();
            });
        }
    }
}
