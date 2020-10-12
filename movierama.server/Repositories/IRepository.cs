using Microsoft.EntityFrameworkCore;
using Movierama.Server.Database.Entities;

namespace Movierama.Server.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        public DbSet<T> Entities { get; }
    }
}
