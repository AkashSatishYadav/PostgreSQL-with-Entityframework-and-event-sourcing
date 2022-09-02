using Microsoft.EntityFrameworkCore;
using PostgresMarten.DataContexts;

namespace PostgresMarten.DataAccess
{
    public interface IDataStore
    {
        PostgresDbContext Context { get; }  
        IEventStorage EventStorage { get; }
        void CommitChanges();
    }
}
