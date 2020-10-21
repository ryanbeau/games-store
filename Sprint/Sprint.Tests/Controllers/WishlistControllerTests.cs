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
    public class WishlistControllerTests : DBContextController
    {
        [Fact]
        public async Task Index_ReturnsViewResult()
        {
            //// Arrange
            //WishlistController wishlistController = new WishlistController(_mockUserManager.Object, _context);

            //// Act
            //var result = await wishlistController.Index();

            //// Assert
            //var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            //Assert.IsAssignableFrom<IEnumerable<UserGameWishlist>>(viewResult.ViewData.Model);
        }
    }
}
