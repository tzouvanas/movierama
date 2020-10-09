using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using movierama.server.Models;
using Movierama.Server.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Services
{
    public class MovieRepository
    {
        private readonly MoviesDbContext context;

        public MovieRepository(MoviesDbContext context)
        {
            this.context = context;
        }

        public List<Movie> GetMovies(string sortOrder)
        {

            List<Movie> movies = null;
            IQueryable<Movie> movieQuery = context.Movies;
            movieQuery = this.ApplySorting(movieQuery, sortOrder);
            movies = movieQuery.ToList();

            return movies;
        }

        public List<Movie> GetMoviesOfUser(string sortOrder, int userId)
        {
            List<Movie> movies = null;
            IQueryable<Movie> movieQuery = context.Movies;
            movieQuery = this.ApplySorting(movieQuery, sortOrder);
            movies = movieQuery.ToList();

            return movies;
        }

        public List<Movie> GetMoviesOfUser(int userId, string sortOrder)
        {
            List<Movie> movies = null;
            IQueryable<Movie> movieQuery = context.Movies.Where(m => m.OwnerId == userId);
            movieQuery = this.ApplySorting(movieQuery, sortOrder);
            movies = movieQuery.ToList();

            return movies;
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

        public string LoadFullDescription(int movieId)
        {
            DescriptionOfMovie descriptionOfMovie = null;

            descriptionOfMovie = context.DescriptionOfMovies
                .Where(item => item.MovieId == movieId)
                .SingleOrDefault();

            return descriptionOfMovie?.Description;
        }
    }
}
