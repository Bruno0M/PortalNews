using Microsoft.EntityFrameworkCore;
using PortalNews.Domain.Entities;
using PortalNews.Infrastructure.Configurations;

namespace PortalNews.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Journalist> Journalists { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<TypeNews> TypeNews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new JournalistConfiguration());
            modelBuilder.ApplyConfiguration(new NewsConfiguration());
            modelBuilder.ApplyConfiguration(new TypeNewsConfiguration());
        }
    }
}
