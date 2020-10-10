using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using movierama.server.Models;
using Movierama.Server.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movierama.Server.Services
{
    public class MovieRepository
    {
        private readonly MoviesDbContext context;

        public MovieRepository(MoviesDbContext context)
        {
            this.context = context;
        }

        public MovieRepository(IConfiguration configuration)
        {
            DbContextOptionsBuilder<MoviesDbContext> builder = new DbContextOptionsBuilder<MoviesDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            this.context = new MoviesDbContext(builder.Options);
        }

        public List<Movie> GetMovies(string userId, string ownerId, string sortOrder)
        {
            List<Movie> movies = null;
            bool userIsAuthenticated = !string.IsNullOrEmpty(userId);
            bool ownerIsProvided = !string.IsNullOrEmpty(ownerId);

            // get movies
            IQueryable<Movie> movieQuery = context.Movies.Include(m => m.Counters);

            // apply search criterion when we are searching for movies of specific owner
            if (ownerIsProvided)
                movieQuery = movieQuery.Where(m => m.OwnerId == ownerId);

            // apply sorting
            movieQuery = this.ApplySorting(movieQuery, sortOrder);

            // run query
            movies = movieQuery.ToList();

            // if user is authenticated get reviews of user for loaded movies
            if (userIsAuthenticated)
            {
                var movieIds = movies.Select(m => m.Id).ToArray();
                var reviewQuery = context.Reviews.Where(r => r.UserId == userId && movieIds.Contains(r.MovieId));
                var reviews = reviewQuery.ToList();
            }

            return movies;
        }

        public void UpdateCounters(Dictionary<int, (int, int, int)> counters)
        {
            var movieIds = counters.Keys.ToArray();
            var counterEntities = this.context.CountersOfMovies.Where(item => movieIds.Contains(item.MovieId));

            foreach (var counterEntity in counterEntities) 
            {
                counterEntity.Likes += counters[counterEntity.MovieId].Item1;
                counterEntity.Hates += counters[counterEntity.MovieId].Item2;
                counterEntity.LastConsideredReviewId = counters[counterEntity.MovieId].Item3;
            }

            this.context.SaveChanges();
        }

        public string GetFullDescription(int movieId)
        {
            DescriptionOfMovie descriptionOfMovie = null;

            descriptionOfMovie = context.DescriptionOfMovies
                .Where(item => item.MovieId == movieId)
                .SingleOrDefault();

            return descriptionOfMovie?.Description;
        }

        private IQueryable<Movie> ApplySorting(IQueryable<Movie> movieQuery, string sortOrder)
        {
            switch (sortOrder)
            {
                case "likes":
                    throw new NotSupportedException("likes sorting is not supported yet");

                case "hates":
                    throw new NotSupportedException("hates sorting is not supported yet");

                case "date":
                    movieQuery = movieQuery.OrderBy(s => s.PublicationDate);
                    break;

                default:
                    movieQuery = movieQuery.OrderByDescending(s => s.Id);
                    break;
            }

            return movieQuery;
        }
    }
}
