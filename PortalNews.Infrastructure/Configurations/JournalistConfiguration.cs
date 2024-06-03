using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalNews.Domain.Entities;

namespace PortalNews.Infrastructure.Configurations
{
    public class JournalistConfiguration : IEntityTypeConfiguration<Journalist>
    {
        public void Configure(EntityTypeBuilder<Journalist> builder)
        {
            builder.ToTable("tb_journalists");

            builder.HasKey(j => j.Id);

            builder.Property(j => j.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(j => j.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(j => j.Email)
                .HasColumnName("email")
                .IsRequired();

            builder.HasIndex(j => j.Email)
                .IsUnique();

            builder.Property(j => j.PasswordSalt)
                .HasColumnName("password_salt")
                .IsRequired();

            builder.Property(j => j.PasswordHash)
                .HasColumnName("password_hash")
                .IsRequired();

            builder.HasMany(j => j.News)
                .WithOne(n => n.Journalist)
                .HasForeignKey(j => j.JournalistId);

        }
    }
}
