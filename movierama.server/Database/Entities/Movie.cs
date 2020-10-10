using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Database.Entities
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public string OwnerId { get; set; }

        public string Title { get; set; }

        public DateTime PublicationDate { get; set; }

        public string Description { get; set; }

        public CountersOfMovie Counters { get; set; }

        public DescriptionOfMovie FullDescription { get; set; }

        public IList<Review> Reviews { get; set; }
    }
}
