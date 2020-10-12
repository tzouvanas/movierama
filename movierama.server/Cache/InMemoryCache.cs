using System.Collections.Generic;

namespace Movierama.Server
{
    public class InMemoryCache<T> : ICache<T> where T : struct
    {
        private Dictionary<string, T> data;

        public InMemoryCache()
        {
            this.data = new Dictionary<string, T>();
        }

        public T? Get(string key)
        {
            if (this.data.ContainsKey(key))
                return this.data[key];
            return null;
        }

        public void Set(string key, T value)
        {
            this.data[key] = value;
        }
    }
}
