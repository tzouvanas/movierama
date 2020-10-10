using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Cache
{
    public class ReviewCache
    {
        public Dictionary<string, Dictionary<int, int>> Data
        {
            get;
            private set;
        }

        public ReviewCache()
        {
            this.Data = new Dictionary<string, Dictionary<int, int>>();
        }
        public void RecordReview(string userId, int movieId, int opinion)
        {
            lock (this)
            {
                // check if user is registered
                if (!this.Data.ContainsKey(userId))
                    this.Data.Add(userId, new Dictionary<int, int>());

                // check if movie is registered
                if (!this.Data[userId].ContainsKey(movieId))
                    this.Data[userId].Add(movieId, opinion);

                this.Data[userId][movieId] = opinion;
            }
        }
    }
}
