using Movierama.Server.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Database.Entities
{
    public enum ReviewType{

        Negative = -1,
        Neutral = 0,
        Positive = 1,
    }

    public class Review
    {
        [Key]
        public int UserId { get; set; }
        
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public ReviewType Type { get; set; }
    }
}
