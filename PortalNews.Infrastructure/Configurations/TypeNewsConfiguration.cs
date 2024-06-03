using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalNews.Domain.Entities;

namespace PortalNews.Infrastructure.Configurations
{
    internal class TypeNewsConfiguration : IEntityTypeConfiguration<TypeNews>
    {
        public void Configure(EntityTypeBuilder<TypeNews> builder)
        {
            builder.ToTable("tb_types_news");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.JournalistId)
                .HasColumnName("journalist_id")
                .IsRequired();

            builder.Property(t => t.TypeName)
                .HasColumnName("type_name")
                .IsRequired();
        }
    }
}
