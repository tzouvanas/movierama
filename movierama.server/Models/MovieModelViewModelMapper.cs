using movierama.server.Models;
using Movierama.Server.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Models
{
    public class MovieModelViewModelMapper
    {
        public MovieModel Map(Movie movie, string userId) 
        {
            bool hasReview = movie.Reviews != null && movie.Reviews.Count > 0;

            var viewModel = new MovieModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                LikeCount = 23,
                HateCount = 45,
                DaysPublished = (int)(DateTime.Today - movie.PublicationDate).TotalDays,
                CanReview = movie.OwnerId != userId,
                ReviewOpinion = hasReview ? movie.Reviews[0].Opinion : ReviewOpinion.Neutral
            };

            return viewModel;
        }

        public List<MovieModel> Map(List<Movie> movies, string userId) 
        {
            var viewModelList = new List<MovieModel>();
            foreach (var movie in movies)
                viewModelList.Add(this.Map(movie, userId));

            return viewModelList;
        }
    }
}
