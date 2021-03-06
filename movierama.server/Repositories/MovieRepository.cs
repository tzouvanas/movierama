﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using movierama.server.Models;
using Movierama.Server.Controllers;
using Movierama.Server.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Repositories
{
    public class MovieRepository : IRepository<Movie>
    {
        private readonly MoviesDbContext context;

        public DbSet<Movie> Entities { get { return this.context.Movies; } }

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

        public async Task<List<Movie>> GetMoviesAsync(string userId, string ownerId, SortBy sortBy, SortOrder sortOrder)
        {
            bool ownerIsProvided = !string.IsNullOrEmpty(ownerId);
            bool userIsAuthenticated = !string.IsNullOrEmpty(userId);

            // start building movies query
            IQueryable<Movie> movieQuery = context.Movies.Include(m => m.Counters);

            // apply search criterion when we are searching for movies of specific owner
            if (ownerIsProvided)
                movieQuery = movieQuery.Where(m => m.OwnerId == ownerId);

            // apply sorting
            movieQuery = this.ApplySorting(movieQuery, sortBy, sortOrder);

            // run query
            var movies = await movieQuery.ToListAsync();

            // if user is authenticated get reviews of user for loaded movies
            if (userIsAuthenticated)
            {
                var movieIds = movies.Select(m => m.Id).ToArray();
                var reviewQuery = context.Reviews.Where(r => r.UserId == userId && movieIds.Contains(r.MovieId));
                var reviews = await reviewQuery.ToListAsync();
            }

            return movies;
        }

        public async Task RegisterNewMovieAsync(Movie movie, string userId)
        {
            movie.OwnerId = userId;
            movie.CreationTime = DateTime.Now;
            // initiate movie counters
            movie.Counters = new CountersOfMovie()
            {
                Hates = 0,
                Likes = 0,
                LastConsideredReviewId = 0
            };

            // handle huge decription scenario.
            if (movie.Description.Length > 300)
            {
                var fullDescription = movie.Description;
                movie.Description = movie.Description.Substring(0, 300) + "...";
                movie.FullDescription = new DescriptionOfMovie() { Description = fullDescription };
            }

            // persist to db
            this.context.Movies.Add(movie);
            await this.context.SaveChangesAsync();
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

        public async Task<string> GetFullDescriptionAsync(int movieId)
        {
            var descriptionEntity = await this.context.DescriptionOfMovies
                .Where(item => item.MovieId == movieId)
                .SingleOrDefaultAsync();

            var result = (descriptionEntity == null) ? string.Empty : descriptionEntity.Description;
            return result;
        }

        private IQueryable<Movie> ApplySorting(IQueryable<Movie> movieQuery, SortBy sortType, SortOrder sortOrder)
        {
            switch (sortType)
            {
                case SortBy.Likes:
                    if (sortOrder == SortOrder.Asc)
                        movieQuery = movieQuery.OrderBy(m => m.Counters.Likes);
                    else
                        movieQuery = movieQuery.OrderByDescending(m => m.Counters.Likes);
                    break;

                case SortBy.Hates:
                    if (sortOrder == SortOrder.Asc)
                        movieQuery = movieQuery.OrderBy(m => m.Counters.Hates);
                    else
                        movieQuery = movieQuery.OrderByDescending(m => m.Counters.Hates);
                    break;

                case SortBy.Date:
                    if (sortOrder == SortOrder.Asc)
                        movieQuery = movieQuery.OrderBy(m => m.CreationTime);
                    else
                        movieQuery = movieQuery.OrderByDescending(m => m.CreationTime);
                    break;

                case SortBy.PublicationDate:
                    if (sortOrder == SortOrder.Asc)
                        movieQuery = movieQuery.OrderBy(m => m.PublicationDate);
                    else
                        movieQuery = movieQuery.OrderByDescending(m => m.PublicationDate);
                    break;
            }

            return movieQuery;
        }
    }
}
