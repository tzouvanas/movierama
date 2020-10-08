using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using movierama.server.Models;
using Movierama.Server.Database.Entities;
using Movierama.Server.Models;
using Movierama.Server.Services;

namespace movierama.server.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> logger;
        private readonly UserManager<MovieramaIdentityUser> userManager;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration,
            UserManager<MovieramaIdentityUser> userManager)
        {
            this.logger = logger;
            this._configuration = configuration;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var user = this.userManager.GetUserAsync(this.User);

            int? userId = null;
            if (user.Result != null)
                userId = user.Result.UserId;

            var movieService = new MovieService(this._configuration);
            var movies = movieService.GetMovies("");

            var mapper = new MovieModelViewModelMapper();
            var movieViewModels = mapper.Map(movies, userId);
            
            return View(movieViewModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
