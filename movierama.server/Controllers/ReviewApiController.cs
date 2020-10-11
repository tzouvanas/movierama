using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using movierama.server.Models;
using Movierama.Server.Cache;
using Movierama.Server.Database;
using Movierama.Server.Models;
using Movierama.Server.Repositories;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movierama.Server.Controllers
{
    [Route("api/review")]
    [ApiController]
    public class ReviewApiController : ControllerBase
    {
        private ILogger<ReviewApiController> logger;
        private IServiceProvider serviceProvider;
        private UserManager<ApplicationIdentityUser> userManager;

        public ReviewApiController(ILogger<ReviewApiController> logger, 
            IServiceProvider serviceProvider,
            UserManager<ApplicationIdentityUser> userManager) 
        {
            this.logger = logger;
            this.userManager = userManager;
            this.serviceProvider = serviceProvider;
        }

        public async Task Review(int movieId, string reviewAction)
        {
            var userId = this.userManager.GetUserId(HttpContext.User);
            var reviewActionValue = Enum.Parse<ReviewAction>(reviewAction);

            // record in redis
            //// var reviewCache = this.serviceProvider.GetService<ReviewCache>();
            //// reviewCache.RecordReviewAction(userId, movieId, (int)reviewActionValue);

            // directly persist in db
            var reviewRepository = new ReviewRepository(this.serviceProvider);
            await reviewRepository.PersistReviewActionAsync(userId, movieId, reviewActionValue);
        }
    }
}
