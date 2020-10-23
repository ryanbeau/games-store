using Microsoft.AspNetCore.Mvc;
using Sprint.Controllers;
using Sprint.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Sprint.Tests.Controllers
{
    public class HomeControllerTests : DBContextController<HomeController>
    {
        public override HomeController CreateControllerSUT()
        {
            return new HomeController(_mockLogger.Object, _context);
        }

        [Fact]
        public async Task Index_ReturnsViewResult()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Index();

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var bannerImages = Assert.IsAssignableFrom<IEnumerable<GameImage>>(viewResult.ViewData["BannerImages"]);
            Assert.NotEmpty(bannerImages);
        }
    }
}
