using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Database.Entities
{
    public class ReviewHistory : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserId { get; set; }

        public int MovieId { get; set; }

        public DateTime CreationTime { get; set; }

        public int Like { get; set; }

        public int Hate { get; set; }
    }
}
