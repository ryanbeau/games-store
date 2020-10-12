using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Sprint.Controllers;
using Sprint.Data;
using System;

namespace Sprint.Tests.Controllers
{
    public abstract class DBContextController : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        protected readonly Mock<ILogger<HomeController>> _mockLogger;

        public DBContextController()
        {
            _mockLogger = new Mock<ILogger<HomeController>>();

            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RDSCGameStore")
                .Options;

            _context = new ApplicationDbContext(contextOptions);
            _context.Database.EnsureCreated();
        }

        public virtual void Dispose()
        {
            _context.Dispose();
        }
    }
}
