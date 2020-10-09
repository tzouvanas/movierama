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

        public int OwnerId { get; set; }

        public string Title { get; set; }

        public DateTime PublicationDate { get; set; }

        public string Description { get; internal set; }
    }
}
