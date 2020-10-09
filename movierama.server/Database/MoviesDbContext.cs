﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using movierama.server.Models;
using Movierama.Server.Database.Entities;

namespace movierama.server.Models
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
            :base(options)
        {
        }

        public DbSet<DescriptionOfMovie> DescriptionOfMovies { get; set; }

        public DbSet<CountersOfMovie> CountersOfMovies { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<User> Users { get; set; }
    }
}