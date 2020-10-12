using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using movierama.server.Models;
using Movierama.Server.Database;
using Movierama.Server.Repositories;
using System;
using System.Threading.Tasks;

namespace Movierama.Server.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MovieApiController : ControllerBase
    {
        private ILogger<MovieApiController> logger;
        private IServiceProvider serviceProvider;
        private UserManager<ApplicationIdentityUser> userManager;

        public MovieApiController(ILogger<MovieApiController> logger,
            IServiceProvider serviceProvider,
            UserManager<ApplicationIdentityUser> userManager)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.serviceProvider = serviceProvider;
        }

        public async Task<string> GetFullDescription(int movieId)
        {
            var movieDbContext = this.serviceProvider.GetService<MoviesDbContext>();
            var movieRepository = new MovieRepository(movieDbContext);
            return await movieRepository.GetFullDescriptionAsync(movieId);
        }
    }
}
