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

            // Preset data for the Providers table
            builder.HasData(
                new Provider { Id = 1, Name = "Поставщик А" },
                new Provider { Id = 2, Name = "Поставщик B" },
                new Provider { Id = 3, Name = "Поставщик C" }
            );
        }
    }
}
