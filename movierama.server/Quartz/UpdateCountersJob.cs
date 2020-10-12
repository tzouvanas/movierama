using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Movierama.Server.Repositories;
using System;

namespace Movierama.Server.Quartz
{
    [DisallowConcurrentExecution]
    public class UpdateCountersJob : IJob
    {
        private readonly ILogger<UpdateCountersJob> logger;
        private readonly IConfiguration configuration;

        public UpdateCountersJob(ILogger<UpdateCountersJob> logger,
            IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                // calculate counters from unprocessed reviews
                var reviewRepository = new ReviewRepository(this.configuration);
                var pendingReviewCounters = reviewRepository.CountPendingReviews();

                // update counters
                var movieRepository = new MovieRepository(this.configuration);
                movieRepository.UpdateCounters(pendingReviewCounters);
            }
            catch(Exception e) 
            {
                // since this is a back end job simply log the error.
                this.logger.LogError(e.ToString());
            }

            return Task.CompletedTask;
        }
    }
}

