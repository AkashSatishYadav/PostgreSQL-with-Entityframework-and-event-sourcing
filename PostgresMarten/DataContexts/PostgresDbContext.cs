using Microsoft.EntityFrameworkCore;
using PostgresMarten.Models;

namespace PostgresMarten.DataContexts
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> option) : base(option)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
