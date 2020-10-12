using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using movierama.server.Models;
using Movierama.Server.Database;
using Movierama.Server.Database.Entities;
using Movierama.Server.Models;
using Movierama.Server.Repositories;
using Movierama.Server.Services;
using Movierama.Server.Views.Home;

namespace movierama.server.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<HomeController> logger;
        private readonly UserManager<ApplicationIdentityUser> userManager;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration,
            UserManager<ApplicationIdentityUser> userManager, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.serviceProvider = serviceProvider;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string ownerId, string sortBy, string sortOrder)
        {
            // store values in ViewBag for html binding
            ViewBag.OwnerId = ownerId;
            
            var sortByValue = SortingHelper.ResolveSortBy(sortBy);
            ViewBag.SortType = sortBy;
            
            var sortOrderValue = SortingHelper.ResolveSortOrder(sortOrder);
            SortingHelper.UpdateViewBag(ViewBag, sortByValue, sortOrderValue);

            // get current user
            var userId = this.userManager.GetUserId(HttpContext.User);
            
            // collect movies
            var movieDbContext = this.serviceProvider.GetService<MoviesDbContext>();
            var movieRepository = new MovieRepository(movieDbContext);
            var movies = await movieRepository.GetMoviesAsync(userId, ownerId, sortByValue, sortOrderValue);

            // get owner names of collected movies
            var userContext = this.serviceProvider.GetService<AuthenticationDbContext>();
            var userRepository = new UserRepository(userContext);
            var ownerNames = await userRepository.GetFullNamesAsync(movies.Select(m => m.OwnerId).ToArray());

            // join data into view model
            var mapper = new MovieViewMapper();
            var movieViewModels = movies.Select(item => mapper.Map(item, userId, ownerNames)).ToList();

            return View(movieViewModels);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Title,PublicationDate,Description")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                var userId = this.userManager.GetUserId(HttpContext.User);
                
                var movieDbContext = this.serviceProvider.GetService<MoviesDbContext>();
                var movieRepository = new MovieRepository(movieDbContext);

                await movieRepository.RegisterNewMovieAsync(movie, userId);
                return RedirectToAction(nameof(Index));
            }

            return View(movie);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
