using Microsoft.AspNetCore.Identity;
using Movierama.Server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Movierama.Server.Views.Shared
{
    public static class FullNameExtension
    {
        public static string ResolveFullName(UserManager<ApplicationIdentityUser> userManager, ClaimsPrincipal principal) {

            var result = FullNameExtension.ResolveFullNameInner(userManager, principal);
            return result.Result;
        }

        private static async Task<string> ResolveFullNameInner(UserManager<ApplicationIdentityUser> userManager, ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            var result = $"{user.FirstName} {user.LastName}";
            return result;
        }
    }
}
