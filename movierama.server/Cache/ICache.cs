using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server
{
    interface ICache<T> where T:struct
    {
        T? Get(string key);

        void Set(string key, T value);
    }
}
