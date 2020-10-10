using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace Movierama.Server.Quartz
{
    [DisallowConcurrentExecution]
    public class UpdateLikeHateCountersJob : IJob
    {
        private readonly ILogger<UpdateLikeHateCountersJob> _logger;
        public UpdateLikeHateCountersJob(ILogger<UpdateLikeHateCountersJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello world!");
            return Task.CompletedTask;
        }
    }
}