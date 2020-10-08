using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Database.Entities
{
    public class MovieramaIdentityUser : IdentityUser
    {
        public int UserId { get; set; }
    }
}
