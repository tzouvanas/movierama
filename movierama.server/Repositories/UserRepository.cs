using Microsoft.AspNetCore.Mvc.Infrastructure;
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

        public string GetFullName(string userId) 
        {
            var user = this.context.Users.Where(item => item.Id == userId).Single();
            return $"{user.FirstName} {user.LastName}";
        }

        public Dictionary<string, string> GetFullNames(string[] userIds)
        {
            var result = new Dictionary<string, string>();
            var users = this.context.Users.Where(item => userIds.Contains(item.Id)).ToList();

            foreach (var user in users)
                result.Add(user.Id, $"{user.FirstName} {user.LastName}");

            return result;
        }
    }
}
