using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string FullDescription { get; set; }

        public string PublicationDate { get; set; }

        public int LikeCounter { get; set; }

        public int HateCounter { get; set; }

        public bool CanVote { get; set; }

        public int? ReviewType { get; set; }

    }
}
