using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movierama.Server.Database.Entities;

namespace movierama.server.Models
{
    public class AuthenticationDbContext : IdentityDbContext<MovieramaIdentityUser>
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
            : base(options)
        {
        }
    }
}
