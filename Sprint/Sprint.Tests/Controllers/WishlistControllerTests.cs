using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sprint.Controllers;
using Sprint.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Sprint.Tests.Controllers
{
    public class WishlistControllerTests : DBContextController<WishlistController>
    {
        public override WishlistController CreateControllerSUT()
        {
            return new WishlistController(_mockUserManager.Object, _context);
        }

        [Fact]
        public async Task Index_ReturnsViewResult()
        {
            // Arrange

            // Act
            //var result = await ControllerSUT.Index();

            // Assert
            //var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            //Assert.IsAssignableFrom<IEnumerable<UserGameWishlist>>(viewResult.ViewData.Model);
        }
    }
}
