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
        public MovieModel Map(Movie movie, string userId, Dictionary<string, string> ownerNames) 
        {
            bool hasCounters = movie.Counters != null;
            bool hasOwner = ownerNames.ContainsKey(movie.OwnerId);
            bool hasReview = movie.Reviews != null && movie.Reviews.Count > 0;

            var viewModel = new MovieModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                OwnerId = movie.OwnerId,
                OwnerFullName = hasOwner ? ownerNames[movie.OwnerId] : string.Empty,
                LikeCount = hasCounters ? movie.Counters.Likes : 0,
                HateCount = hasCounters ? movie.Counters.Hates : 0,
                PublicationDate = movie.PublicationDate.ToString("dd-MM-yyyy"),
                PostDuration = movie.CreationTime.TimeDistanceFromNow().Item1,
                UnitOfPostDuration = movie.CreationTime.TimeDistanceFromNow().Item2,
                CanReview = movie.OwnerId != userId,
                ReviewOpinion = hasReview ? movie.Reviews[0].Opinion : ReviewOpinion.Neutral
            };

            return viewModel;
        }
    }
}
