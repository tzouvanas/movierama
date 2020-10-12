using System;
using System.Collections.Generic;

namespace Movierama.Server.Cache
{
    public class ReviewCache : MemoryCache
    {
        public ReviewCache()
            :base()
        {
        }

        public void RecordReviewAction(string userId, int movieId, int reviewAction)
        {
            // check if user is registered
            if (!this.Data.ContainsKey(userId))
                this.Data.Add(userId, new Dictionary<int, Queue<int>>());

            // check if movie is registered
            if (!this.Data[userId].ContainsKey(movieId))
                this.Data[userId].Add(movieId, new Queue<int>());

            this.Data[userId][movieId].Enqueue(reviewAction);
        }
    }
}
