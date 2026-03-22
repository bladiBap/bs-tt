using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolTestBackend.Core.Abstractions;
using SolTestBackend.Domain.Currencies.Entities;
using SolTestBackend.Domain.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolTestBackend.Infrastructure.Persistence.DomainModel.Config
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(Product.MaxNameLength);

            builder.Property(x => x.Sku)
                .HasColumnName("sku")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Stock)
                .HasColumnName("stock")
                .IsRequired();

            builder.Property(x => x.CurrencyId)
                .HasColumnName("currency_id")
                .IsRequired();

            
            builder.OwnsOne(x => x.Price, price =>
            {
                price.Property(p => p.Value)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
            });

            builder.Property(c => c.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(c => c.UpdatedAt)
                .HasColumnName("updated_at");

            builder.HasIndex(x => x.Sku).IsUnique();

            builder.HasOne<Currency>()
            .WithMany()
            .HasForeignKey(p => p.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
