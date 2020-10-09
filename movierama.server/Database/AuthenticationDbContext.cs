using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movierama.Server.Database;

namespace movierama.server.Models
{
    public class AuthenticationDbContext : IdentityDbContext<ApplicationIdentityUser>
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
            : base(options)
        {
        }
    }
}
