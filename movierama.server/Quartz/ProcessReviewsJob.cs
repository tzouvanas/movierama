using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Logging;
using Movierama.Server.Cache;
using Quartz;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Movierama.Server.Repositories;
using Movierama.Server.Models;
using movierama.server.Models;
using Microsoft.Extensions.Configuration;

namespace Movierama.Server.Quartz
{
    [DisallowConcurrentExecution]
    public class ProcessReviewsJob : IJob
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IConfiguration configuration;
        private readonly ILogger<ProcessReviewsJob> _logger;
        public ProcessReviewsJob(ILogger<ProcessReviewsJob> logger, 
            IServiceProvider serviceProvider, 
            IConfiguration configuration)
        {
            _logger = logger;
            this.serviceProvider = serviceProvider;
            this.configuration = configuration;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var reviewRepository = new ReviewRepository(this.configuration);
            var reviewCache = this.serviceProvider.GetService<ReviewCache>();
            
            foreach (string userId in reviewCache.Data.Keys) {
                foreach (int movieId in reviewCache.Data[userId].Keys) {
                    var opinion = reviewCache.Data[userId][movieId];
                    reviewRepository.RegisterReview(userId, movieId, (ReviewOpinion)opinion);
                }
            }

            return Task.CompletedTask;
        }
    }
}