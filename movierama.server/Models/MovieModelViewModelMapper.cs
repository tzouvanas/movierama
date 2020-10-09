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
            var viewModel = new MovieModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                LikeCount = 23,
                HateCount = 45,
                DaysPublished = (int)(DateTime.Today - movie.PublicationDate).TotalDays,
                CanVote = movie.OwnerId != userId
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
