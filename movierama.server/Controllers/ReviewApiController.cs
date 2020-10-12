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

            var isValidRequest = this.IsValidReviewAction(userId, movieId, reviewAction);
            if (!isValidRequest) throw new ArgumentException("Invalid review action", "reviewAction");

            var reviewActionValue = Enum.Parse<ReviewAction>(reviewAction);

            // persist in db
            var reviewRepository = new ReviewRepository(this.serviceProvider);
            await reviewRepository.PersistReviewActionAsync(userId, movieId, reviewActionValue);

            // record value in cache
            var reviewActionCache = this.serviceProvider.GetService<ReviewActionCache>();
            reviewActionCache.Set(userId, movieId, reviewActionValue);
        }

        private bool IsValidReviewAction(string userId, int movieId, string reviewAction) 
        {
            ReviewAction reviewActionValue;
            var isValidString = Enum.TryParse<ReviewAction>(reviewAction, out reviewActionValue);

            if (!isValidString) return false;

            var reviewActionCache = this.serviceProvider.GetService<ReviewActionCache>();

            var lastReviewAction = reviewActionCache.Get(userId, movieId);

            if (!lastReviewAction.HasValue)
                return true;

            bool result = true;
            
            if (lastReviewAction.HasValue) 
            {
                switch (lastReviewAction.Value) 
                {
                    case ReviewAction.Like:
                        result = reviewActionValue == ReviewAction.Unlike;
                        break;

                    case ReviewAction.Hate:
                        result = reviewActionValue == ReviewAction.Unhate;
                        break;

                    case ReviewAction.Unlike:
                    case ReviewAction.Unhate:
                        result = reviewActionValue == ReviewAction.Like || reviewActionValue == ReviewAction.Hate;
                        break;
                }
            }

            return result;
        }
    }
}
