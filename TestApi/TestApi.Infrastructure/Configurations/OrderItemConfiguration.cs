using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApi.Domain.Entities;

namespace TestApi.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.Name)
                .IsRequired();

            builder.Property(oi => oi.Quantity)
                .IsRequired()
                .HasColumnType("decimal(18,3)");

            builder.Property(oi => oi.Unit)
                .IsRequired();
        }
    }
}
