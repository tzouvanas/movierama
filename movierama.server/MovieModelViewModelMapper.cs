using ExtensionMethods;
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
            bool hasCounters = movie.Counters != null;

            var viewModel = new MovieModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                LikeCount = hasCounters ? movie.Counters.Likes : 0,
                HateCount = hasCounters ? movie.Counters.Hates : 0,
                PublicationDuration = movie.PublicationDate.TimeAgo().Item1,
                UnitOfPulicationDuration = movie.PublicationDate.TimeAgo().Item2,
                CanReview = movie.OwnerId != userId,
                ReviewOpinion = hasReview ? movie.Reviews[0].Opinion : ReviewOpinion.Neutral
            };

            return viewModel;
        }
    }
}
