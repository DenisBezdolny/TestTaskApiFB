using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApi.Domain.Entities;

namespace TestApi.Infrastructure.Configurations
{
    public class ProviderConfiguration : IEntityTypeConfiguration <Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.ToTable("Providers");
            
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired();

            builder.HasMany(p => p.Orders)
                .WithOne(o => o.Provider)
                .HasForeignKey(o => o.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
