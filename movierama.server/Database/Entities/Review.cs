using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace movierama.server.Models
{
    public enum ReviewType{

        Negative = -1,
        Neutral = 0,
        Positive = 1,
    }

    public class Review
    {
        [Key]
        int UserId { get; set; }
        
        [Key]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public ReviewType Type { get; set; }
    }
}
