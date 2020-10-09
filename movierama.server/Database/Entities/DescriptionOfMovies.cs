using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Database.Entities
{
    public class DescriptionOfMovie
    {
        [Key]
        public int MovieId { get; set; }

        public string Description { get; set; }
    }
}
