using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using movierama.server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Repositories
{
    public class UserRepository
    {
        private AuthenticationDbContext context;

        public UserRepository(AuthenticationDbContext context) {
            this.context = context;
        }

        public async Task<Dictionary<string, string>> GetFullNamesAsync(string[] userIds)
        {
            var result = new Dictionary<string, string>();
            var users = await this.context.Users.Where(item => userIds.Contains(item.Id)).ToListAsync();

            foreach (var user in users)
                result.Add(user.Id, $"{user.FirstName} {user.LastName}");

            return result;
        }
    }
}
