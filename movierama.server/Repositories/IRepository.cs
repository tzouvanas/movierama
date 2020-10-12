using Microsoft.EntityFrameworkCore;
using Movierama.Server.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movierama.Server.Repositories
{
    public interface IRepository<T> where T: Entity
    {
        public DbSet<T> Entities { get; }
    }
}
