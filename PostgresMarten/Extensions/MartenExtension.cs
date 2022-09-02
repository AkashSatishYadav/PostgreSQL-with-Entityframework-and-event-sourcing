using Marten;
using PostgresMarten.DataAccess;

namespace PostgresMarten.Extensions
{
    public static class MartenExtension
    {
        public static void AddMarten(this IServiceCollection services, string cnnString)
        {
            services.AddSingleton(CreateDocumentStore(cnnString));
            services.AddScoped<IDataStore, MartenDataStore>();
        }

        private static IDocumentStore CreateDocumentStore(string connectionString)
        {
            var store = DocumentStore.For(_ =>
            {
                _.Connection(connectionString);
            });

            return store;
        }
    }
}
