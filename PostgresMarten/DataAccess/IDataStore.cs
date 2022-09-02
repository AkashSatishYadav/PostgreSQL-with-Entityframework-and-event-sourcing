using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PostgresMarten.DataContexts;

namespace PostgresMarten.DataAccess
{
    public interface IDataStore
    {
        IDistributedCache Cache { get; }
        PostgresDbContext Context { get; }  
        IEventStorage EventStorage { get; }
        void CommitChanges();
    }
}
