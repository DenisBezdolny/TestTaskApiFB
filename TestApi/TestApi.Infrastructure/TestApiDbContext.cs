using Microsoft.EntityFrameworkCore;
using TestApi.Domain.Entities;
using TestApi.Infrastructure.Configurations;

namespace TestApi.Infrastructure
{
    public class TestApiDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrederItems { get; set; }
        public DbSet<Provider> Providers { get; set; }

        public TestApiDbContext(DbContextOptions dbContextDbOptions) : base(dbContextDbOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProviderConfiguration());
        }
    }
}
