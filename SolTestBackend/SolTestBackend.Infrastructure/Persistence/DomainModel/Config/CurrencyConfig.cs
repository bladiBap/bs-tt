using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolTestBackend.Domain.Currencies.Entities;


namespace SolTestBackend.Infrastructure.Persistence.DomainModel.Config
{
    internal class CurrencyConfig : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("currencies");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Symbol)
                .HasColumnName("symbol")
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(c => c.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(c => c.UpdatedAt)
                .HasColumnName("updated_at");

            builder.HasIndex(x => x.Symbol).IsUnique();
        }
    }
}
