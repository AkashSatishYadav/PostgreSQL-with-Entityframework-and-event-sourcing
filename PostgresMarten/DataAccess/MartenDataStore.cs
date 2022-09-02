using Marten;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PostgresMarten.DataContexts;

namespace PostgresMarten.DataAccess
{
    public class MartenDataStore : IDataStore
    {
        public MartenDataStore(IDocumentStore documentStore, PostgresDbContext context, IDistributedCache cache)
        {
            Cache = cache;
            _session = documentStore.OpenSession();
            Context = context;
            EventStorage = new EventStorage(_session.Events);
        }
        private readonly IDocumentSession _session;
        public PostgresDbContext Context { get; }

        public IEventStorage EventStorage { get; }
        public IDistributedCache Cache { get; }
        public void CommitChanges()
        {
            _session.SaveChanges();
        }
    }
}
