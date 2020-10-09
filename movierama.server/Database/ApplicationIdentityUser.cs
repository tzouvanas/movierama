using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movierama.Server.Database
{
    public class ApplicationIdentityUser : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
