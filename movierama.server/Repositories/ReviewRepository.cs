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

        public ReviewRepository(IConfiguration configuration)
        {
            DbContextOptionsBuilder<MoviesDbContext> builder = new DbContextOptionsBuilder<MoviesDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            this.context = new MoviesDbContext(builder.Options);
        }

        public void RegisterReview(string userId, int movieId, ReviewOpinion opinion)
        {
            var existingReview = this.context.Reviews
                .Where(r => r.UserId == userId && r.MovieId == movieId)
                .SingleOrDefault();

            if (existingReview != null)
            {
                existingReview.Opinion = (int)opinion;
            }
            else
            {
                var review = new Review() { MovieId = movieId, UserId = userId, Opinion = (int)opinion };
                context.Reviews.Add(review);
            }

            context.SaveChanges();
        }
    }
}
