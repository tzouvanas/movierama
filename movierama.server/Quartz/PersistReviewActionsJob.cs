using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Logging;
using Movierama.Server.Cache;
using Quartz;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Movierama.Server.Repositories;
using Movierama.Server.Models;
using Microsoft.Extensions.Configuration;

namespace Movierama.Server.Quartz
{
    [DisallowConcurrentExecution]
    public class PersistReviewActionsJob : IJob
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IConfiguration configuration;
        private readonly ILogger<PersistReviewActionsJob> logger;
        public PersistReviewActionsJob(ILogger<PersistReviewActionsJob> logger, 
            IServiceProvider serviceProvider, 
            IConfiguration configuration)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            this.configuration = configuration;
        }
        public Task Execute(IJobExecutionContext context)
        {
            
            var reviewCache = this.serviceProvider.GetService<ReviewCache>();

            // TODO : consider batch update instead of review by review update
            foreach (string userId in reviewCache.Data.Keys) 
            {
                foreach (int movieId in reviewCache.Data[userId].Keys) 
                {
                    var reviewActionQueue = reviewCache.Data[userId][movieId];

                    if (reviewActionQueue.Count == 0) continue;

                    while (reviewActionQueue.Count > 0) 
                    {
                        var reviewRepository = new ReviewRepository(this.configuration);
                        var reviewAction = reviewActionQueue.Peek();
                        var task = reviewRepository.PersistReviewActionAsync(userId, movieId, (ReviewAction)reviewAction);
                        task.Start();
                        task.Wait();
                        reviewActionQueue.Dequeue();
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}