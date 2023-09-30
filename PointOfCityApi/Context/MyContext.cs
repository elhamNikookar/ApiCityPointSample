using Microsoft.EntityFrameworkCore;
using PointOfCityApi.Models;

namespace PointOfCityApi.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        
        public DbSet<City> Cities { get; set; }
        public DbSet<Point> Points { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Point>()
                .HasOne<City>(p => p.City)
                .WithMany(c => c.Points)
                .HasForeignKey(p => p.CityID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
