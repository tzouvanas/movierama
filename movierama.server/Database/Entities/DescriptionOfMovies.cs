﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movierama.Server.Database.Entities
{
    public class DescriptionOfMovie : Entity
    {
        [Key]
        public int MovieId { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

        public string Description { get; set; }
    }
}
