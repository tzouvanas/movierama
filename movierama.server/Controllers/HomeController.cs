﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using movierama.server.Models;
using Movierama.Server.Database;
using Movierama.Server.Models;
using Movierama.Server.Services;

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

        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(HttpContext.User);

            var movieDbContext = this.serviceProvider.GetService<MoviesDbContext>();
            var movieRepository = new MovieRepository(movieDbContext);
            var movies = movieRepository.GetMovies(userId, string.Empty, string.Empty);

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
