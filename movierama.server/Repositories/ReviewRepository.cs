using movierama.server.Models;
using Movierama.Server.Database.Entities;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Movierama.Server.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.CompilerServices;

namespace Movierama.Server.Repositories
{
    public class ReviewRepository : IRepository<Review>
    {
        private MoviesDbContext context;
        private IServiceProvider serviceProvider;

        public DbSet<Review> Entities { get { return this.context.Reviews; } }

        public ReviewRepository(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.context = this.serviceProvider.GetService<MoviesDbContext>();
        }

        public ReviewRepository(IConfiguration configuration)
        {
            DbContextOptionsBuilder<MoviesDbContext> builder = new DbContextOptionsBuilder<MoviesDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            this.context = new MoviesDbContext(builder.Options);
        }

        public async Task PersistReviewActionAsync(string userId, int movieId, ReviewAction reviewAction)
        {
            // create review history item
            var reviewHistory = new ReviewHistory()
            {
                MovieId = movieId,
                UserId = userId,
                CreationTime = DateTime.Now,
                Like = reviewAction == ReviewAction.Like ? 1 : (reviewAction == ReviewAction.Unlike ? -1 : 0),
                Hate = reviewAction == ReviewAction.Hate ? 1 : (reviewAction == ReviewAction.Unhate ? -1 : 0)
            };

            this.context.ReviewsHistory.Add(reviewHistory);

            // Create or update review
            var review = this.context.Reviews.Where(r => r.UserId == userId && r.MovieId == movieId).SingleOrDefault();

            if (review == null)
            {
                review = new Review()
                {
                    MovieId = movieId,
                    UserId = userId,
                    Opinion = reviewAction == ReviewAction.Like ? ReviewOpinion.Like :
                    (reviewAction == ReviewAction.Hate ? ReviewOpinion.Hate : ReviewOpinion.Neutral)
                };

                this.context.Reviews.Add(review);
            }
            else
            {
                review.Opinion = reviewAction == ReviewAction.Like ? ReviewOpinion.Like :
                    (reviewAction == ReviewAction.Hate ? ReviewOpinion.Hate : ReviewOpinion.Neutral);
            }

            await this.context.SaveChangesAsync();
        }

        public Dictionary<int, (int, int, int)> CountPendingReviews()
        {
            var query =
                from reviewHistory in this.context.ReviewsHistory
                from counter in this.context.CountersOfMovies
                .Where(c => c.MovieId == reviewHistory.MovieId && c.LastConsideredReviewId < reviewHistory.Id)
                group reviewHistory by reviewHistory.MovieId into g
                select new
                {
                    g.Key,
                    NewLikes = g.Sum(i => i.Like),
                    NewHates = g.Sum(i => i.Hate),
                    Checkpoint = g.Max(i => i.Id)
                };

            var queryResults = query.ToList();

            var dictionary = new Dictionary<int, (int, int, int)>();
            queryResults.ForEach(item => dictionary.Add(item.Key, (item.NewLikes, item.NewHates, item.Checkpoint)));

            return dictionary;
        }
    }
}
