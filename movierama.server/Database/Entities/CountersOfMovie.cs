using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movierama.Server.Database.Entities
{
    public class CountersOfMovie : Entity
    {
        [Key]
        public int MovieId { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

        public int Likes { get; set; }

        public int Hates { get; set; }

        public int LastConsideredReviewId { get; set; }
    }
}
