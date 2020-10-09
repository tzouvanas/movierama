using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movierama.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private ILogger<ReviewController> logger;
        public ReviewController(ILogger<ReviewController> logger) 
        {
            this.logger = logger;
        }

        [HttpGet]
        public void Review(int movieId, bool like)
        {
            // keep internal cache with userId - movieId - status
            var datet = DateTime.Now;
            this.logger.LogError(datet.ToString());
        }
    }
}
