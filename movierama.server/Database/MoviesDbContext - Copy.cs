using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using movierama.server.Models;
using Movierama.Server.Database.Entities;

namespace movierama.server.Models
{
    public class TransientMoviesDbContext : MoviesDbContext
    {
        public TransientMoviesDbContext(DbContextOptions<MoviesDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
