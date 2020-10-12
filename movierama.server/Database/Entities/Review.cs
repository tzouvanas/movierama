using Movierama.Server.Database.Entities;
using Movierama.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Database.Entities
{
    public class Review : Entity
    {
        [Key]
        public string UserId { get; set; }

        [Key]
        public int MovieId { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

        public ReviewOpinion Opinion { get; set; }
    }
}
