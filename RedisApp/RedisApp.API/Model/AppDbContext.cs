using Microsoft.EntityFrameworkCore;

namespace RedisApp.API.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasData(
                new Product { Id = 1, Name = "Motor", Price = 1200 },
                new Product { Id = 2, Name = "Parça", Price = 1200 },
                new Product { Id = 3, Name = "Robot Süpürge", Price = 1200 },
                new Product { Id = 4, Name = "Telefon", Price = 1200 }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}