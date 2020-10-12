using Microsoft.EntityFrameworkCore;
using Movierama.Server.Database.Entities;

namespace movierama.server.Models
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().HasKey(r => new { r.MovieId, r.UserId });
        }

        public DbSet<DescriptionOfMovie> DescriptionOfMovies { get; set; }

        public DbSet<CountersOfMovie> CountersOfMovies { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<ReviewHistory> ReviewsHistory { get; set; }
    }
}
