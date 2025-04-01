using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApi.Domain.Entities;

namespace TestApi.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Number)
                .IsRequired();

            builder.Property(o => o.Date)
                .IsRequired()
                .HasColumnType("datetime2(7)");

            builder.Property(o => o.ProviderId)
                .IsRequired();

            builder.HasIndex(o => new { o.Number, o.ProviderId })
                .IsUnique();

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
