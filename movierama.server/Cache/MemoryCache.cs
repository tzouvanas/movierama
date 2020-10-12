using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Movierama.Server
{
    public class MemoryCache : ICache
    {
        public Dictionary<string, Dictionary<int, Queue<int>>> Data
        {
            get;
            private set;
        }

        public MemoryCache() {
            this.Data = new Dictionary<string, Dictionary<int, Queue<int>>>();
        }
    }
}
