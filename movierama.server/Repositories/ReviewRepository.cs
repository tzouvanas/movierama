using movierama.server.Models;
using Movierama.Server.Database.Entities;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Movierama.Server.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Movierama.Server.Repositories
{
    public class ReviewRepository
    {
        private MoviesDbContext context;
        private IServiceProvider serviceProvider;

        public ReviewRepository(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.context = this.serviceProvider.GetService<MoviesDbContext>();
        }

        public async Task RegisterReview(string userId, int movieId, ReviewOpinion opinion)
        {
            var existingReview = await this.context.Reviews
                .Where(r => r.UserId == userId && r.MovieId == movieId)
                .SingleOrDefaultAsync();

            if (existingReview != null)
            {
                existingReview.Opinion = (int)opinion;
            }
            else
            {
                var review = new Review() { MovieId = movieId, UserId = userId, Opinion = (int)opinion };
                context.Reviews.Add(review);
            }

            await context.SaveChangesAsync();
        }
    }
}
