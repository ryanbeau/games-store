using Microsoft.AspNetCore.Mvc;
using Sprint.Controllers;
using Sprint.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Sprint.Tests.Controllers
{
    public class HomeControllerTests : DBContextController
    {
        [Fact]
        public async Task Index_ReturnsViewResult()
        {
            // Arrange
            HomeController homeController = new HomeController(_mockLogger.Object, _context);

            // Act
            var result = await homeController.Index();

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var bannerImages = Assert.IsAssignableFrom<IEnumerable<GameImage>>(viewResult.ViewData["BannerImages"]);
            Assert.NotEmpty(bannerImages);
        }
    }
}
