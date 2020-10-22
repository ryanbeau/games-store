using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Sprint.Data;
using Sprint.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sprint.Tests.Controllers
{
    public abstract class DBContextController<TController> : IDisposable where TController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected readonly Mock<ILogger<TController>> _mockLogger;
        protected readonly Mock<UserManager<User>> _mockUserManager;

        protected User GetUserAsyncReturns { get; set; }
        protected User FindByNameAsyncReturns { get; set; }

        protected TController ControllerSUT { get; }

        public DBContextController()
        {
            _mockLogger = new Mock<ILogger<TController>>();
            _mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);

            // GetUserAsync
            _mockUserManager.Setup(s => s.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .Returns(() => Task.FromResult(GetUserAsyncReturns));

            // FindByNameAsync
            _mockUserManager.Setup(s => s.FindByNameAsync(It.IsAny<string>()))
                .Returns(() => Task.FromResult(FindByNameAsyncReturns));

            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(contextOptions);
            _context.Database.EnsureCreated();

            ControllerSUT = CreateControllerSUT();
            ControllerSUT.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
        }

        public abstract TController CreateControllerSUT();

        public virtual void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
