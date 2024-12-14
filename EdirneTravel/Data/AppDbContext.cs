using EdirneTravel.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EdirneTravel.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Place> Places { get; set; }
        public DbSet<UserFav> UserFavs { get; set; }
        public DbSet<TravelRoute> Routes { get; set; }
        public DbSet<TravelRoutePlace> TravelRoutePlace { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFav>()
           .HasOne(uf => uf.User)
           .WithMany(u => u.UserFavs)
           .HasForeignKey(uf => uf.UserId);

            modelBuilder.Entity<UserFav>()
                .HasOne(uf => uf.Place)
                .WithMany(p => p.UserFavs)
                .HasForeignKey(uf => uf.PlaceId);

            // TravelRoute Configuration
            modelBuilder.Entity<TravelRoute>()
                .HasOne(r => r.User)
                .WithMany(u => u.Routes)
                .HasForeignKey(r => r.UserId);


            // TravelRoutePlace Configuration
            modelBuilder.Entity<TravelRoutePlace>()
                .HasOne(rp => rp.TravelRoute)
                .WithMany(r => r.TravelRoutePlaces)
                .HasForeignKey(rp => rp.TravelRouteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TravelRoutePlace>()
                .HasOne(rp => rp.Place)
                .WithMany(p => p.RoutePlaces)
                .HasForeignKey(rp => rp.PlaceId);

            // Category Config
            modelBuilder.Entity<TravelRoute>()
        .HasOne(p => p.Category)
        .WithMany(c => c.Routes)
        .HasForeignKey(p => p.CategoryId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
