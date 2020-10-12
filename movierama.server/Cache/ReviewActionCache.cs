using Movierama.Server.Models;
using System;
using System.Collections.Generic;

namespace Movierama.Server.Cache
{
    public class ReviewActionCache : InMemoryCache<ReviewAction>
    {
        public ReviewActionCache()
            :base()
        {
        }

        public ReviewAction? Get(string userId, int movieId) {

            var cacheKey = $"{userId}_{movieId}";
            return this.Get(cacheKey);
        }

        public void Set(string userId, int movieId, ReviewAction reviewAction)
        {
            var cacheKey = $"{userId}_{movieId}";
            this.Set(cacheKey, reviewAction);
        }
    }
}
