﻿using Movierama.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movierama.Server.Database.Entities
{
    public class Movie : Entity
    {
        [Key]
        public int Id { get; set; }

        public string OwnerId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        public string Title { get; set; }

        public DateTime CreationTime { get; set; }


        [Required]
        [DateInThePast]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        [Required]
        [StringLength(9000, ErrorMessage = "Name length can't be more than 9000.")]
        public string Description { get; set; }

        public CountersOfMovie Counters { get; set; }

        public DescriptionOfMovie FullDescription { get; set; }

        public IList<Review> Reviews { get; set; }
    }
}
