using System;
using System.Collections.Generic;

namespace Movierama.Server.Cache
{
    public class ReviewCache
    {
        public Dictionary<string, Dictionary<int, Queue<int>>> Data
        {
            get;
            private set;
        }

        public ReviewCache()
        {
            this.Data = new Dictionary<string, Dictionary<int, Queue<int>>>();
        }
        public void RecordReviewAction(string userId, int movieId, int reviewAction)
        {
            lock (this)
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
}
