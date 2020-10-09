using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Controllers
{
    public class rev
    {
        [Microsoft.AspNetCore.Mvc.FromBody]
        public int MovieId { get; set; }

        [Microsoft.AspNetCore.Mvc.FromBody]
        public bool Like { get; set; }
    }
}
