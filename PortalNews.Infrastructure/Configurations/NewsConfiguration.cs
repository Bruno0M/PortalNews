using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNews.Infrastructure.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("tb_news");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.JournalistId)
                .HasColumnName("journalist_id")
                .IsRequired();

            builder.Property(n => n.TypeNewsId)
                .HasColumnName("type_news_id")
                .IsRequired();

            builder.Property(n => n.Title)
                .HasColumnName("title")
                .IsRequired();

            builder.Property(n => n.Description)
                .HasColumnName("description")
                .HasMaxLength(500);

            builder.Property(n => n.NewsBody)
                .HasColumnName("news_body")
                .IsRequired();

            builder.Property(n => n.PublishedDate)
                .HasColumnType("date")
                .HasColumnName("published_date")
                .IsRequired();

            builder.HasOne(n => n.Journalist)
                .WithMany(j => j.News);
        }
    }
}
