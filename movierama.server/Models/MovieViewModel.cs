﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        public string OwnerFullName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int DaysPublished { get; set; }

        public int LikeCount { get; set; }

        public int HateCount { get; set; }

        public bool CanVote { get; set; }

        public int? ReviewType { get; set; }

    }
}