using Microsoft.EntityFrameworkCore;
using Sprint.Models;

namespace Sprint.Tests.Controllers
{
    public abstract class DBContextController
    {
        public DbContextOptions<GameStoreContext> ContextOptions { get; }

        public DBContextController()
        {
            ContextOptions = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase(databaseName: "game_store")
                .Options;

            Seed();
        }

        /// <summary>
        /// Populate DBContext with data.
        /// </summary>
        protected abstract void Seed();
    }
}
