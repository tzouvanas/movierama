using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using movierama.server.Models;

namespace movierama.server.Models
{
    public class MovieramaDbContext : DbContext
    {
        private readonly string connectionString;

        public MovieramaDbContext(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<DescriptionOfMovie> DescriptionOfMovies { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Review> Reviews { get; set; }
    }
}
