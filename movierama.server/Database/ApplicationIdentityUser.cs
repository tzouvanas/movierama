using Microsoft.AspNetCore.Identity;

namespace Movierama.Server.Database
{
    public class ApplicationIdentityUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }
    }
}
