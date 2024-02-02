using RpaAeC.Data;
using RpaAeC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace RpaAeC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SearchTraining> Training { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SearchResultEntityTypeConfiguration());
        }
    }
}
