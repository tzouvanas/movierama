using Movierama.Server.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Database.Entities
{
    public class Review
    {
        [Key]
        public string UserId { get; set; }

        [Key]
        public int MovieId { get; set; }


        public int Opinion { get; set; }
    }
}
