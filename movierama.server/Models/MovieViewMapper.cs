using ExtensionMethods;
using Movierama.Server.Database.Entities;
using System.Collections.Generic;

namespace Movierama.Server.Models
{
    public class MovieViewMapper
    {
        public MovieViewModel Map(Movie movie, string userId, Dictionary<string, string> ownerNames)
        {
            bool hasCounters = movie.Counters != null;
            bool hasOwner = ownerNames.ContainsKey(movie.OwnerId);
            bool hasReview = movie.Reviews != null && movie.Reviews.Count > 0;

            var viewModel = new MovieViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                OwnerId = movie.OwnerId,
                OwnerFullName = hasOwner ? ownerNames[movie.OwnerId] : string.Empty,
                LikeCount = hasCounters ? movie.Counters.Likes : 0,
                HateCount = hasCounters ? movie.Counters.Hates : 0,
                PublicationDate = movie.PublicationDate.ToString("dd-MM-yyyy"),
                PostDuration = movie.CreationTime.TimeAgo().Item1,
                UnitOfPostDuration = movie.CreationTime.TimeAgo().Item2,
                CanReview = movie.OwnerId != userId,
                ReviewOpinion = hasReview ? movie.Reviews[0].Opinion : ReviewOpinion.Neutral
            };

            return viewModel;
        }
    }
}
