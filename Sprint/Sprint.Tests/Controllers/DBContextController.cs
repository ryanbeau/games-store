using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Sprint.Controllers;
using Sprint.Data;
using Sprint.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sprint.Tests.Controllers
{
    public abstract class DBContextController : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        protected readonly Mock<ILogger<HomeController>> _mockLogger;
        protected readonly Mock<UserManager<User>> _mockUserManager;

        protected User UserFromUserManager { get; set; } = new User { Id = 1 };

        public DBContextController()
        {
            var mockStore = new Mock<IUserStore<User>>();

            _mockLogger = new Mock<ILogger<HomeController>>();
            _mockUserManager = new Mock<UserManager<User>>(mockStore.Object, null, null, null, null, null, null, null, null);

            _mockUserManager.Setup(s => s.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .Returns(UserManagerGetUserAsync);

            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RDSCGameStore")
                .Options;

            _context = new ApplicationDbContext(contextOptions);
            _context.Database.EnsureCreated();
        }

        protected virtual Task<User> UserManagerGetUserAsync()
        {
            return Task.FromResult(UserFromUserManager);
        }

        public virtual void Dispose()
        {
            _context.Dispose();
        }
    }
}
