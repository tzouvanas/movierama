using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Cache
{
    public class ReviewCache
    {
        private Dictionary<string, Dictionary<int, int>> cache;

        public ReviewCache() {
            this.cache = new Dictionary<string, Dictionary<int, int>>();
        }
        public void RecordReview(string userId, int movieId, int opinion) 
        {
            lock (this)
            {
                // check if user is registered
                if (!this.cache.ContainsKey(userId))
                    this.cache.Add(userId, new Dictionary<int, int>());

                // check if movie is registered
                if (!this.cache[userId].ContainsKey(movieId))
                    this.cache[userId].Add(movieId, opinion);

                this.cache[userId][movieId] = opinion;
            }
        }
    }
}
