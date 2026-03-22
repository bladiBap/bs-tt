using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolTestBackend.Domain.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolTestBackend.Infrastructure.Persistence.DomainModel.Config
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(150);

            builder.OwnsOne(x => x.Email, email =>
            {
                email.Property(e => e.Email)
                    .HasColumnName("email")
                    .IsRequired()
                    .HasMaxLength(255);

                email.HasIndex(e => e.Email).IsUnique();
            });

            builder.OwnsOne(x => x.Password, password =>
            {
                password.Property(p => p.Hash)
                    .HasColumnName("password")
                    .IsRequired();
            });

            builder.Property(c => c.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(c => c.UpdatedAt)
                .HasColumnName("updated_at");
        }
    }
}
