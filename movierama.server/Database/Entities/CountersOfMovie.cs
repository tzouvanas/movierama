using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Database.Entities
{
    public class CountersOfMovie
    {
        [Key]
        public int MovieId { get; set; }

        public int LikeCounter { get; set; }

        public int HateCounter { get; set; }
    }
}
