using Movierama.Server.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Models
{
    public class MovieModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string OwnerFullName { get; set; }

        public int PublicationDuration { get; set; }

        public string UnitOfPulicationDuration { get; set; }

        public int LikeCount { get; set; }

        public int HateCount { get; set; }

        public bool CanReview { get; set; }

        public ReviewOpinion ReviewOpinion { get; set; }

    }
}
