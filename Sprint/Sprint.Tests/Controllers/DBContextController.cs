using Microsoft.EntityFrameworkCore;
using Sprint.Data;

namespace Sprint.Tests.Controllers
{
    public abstract class DBContextController
    {
        public DbContextOptions<ApplicationDbContext> ContextOptions { get; }

        public DBContextController()
        {
            ContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RDSCGameStore")
                .Options;

            Seed();
        }

        /// <summary>
        /// Populate DBContext with data.
        /// </summary>
        protected abstract void Seed();
    }
}
