using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sprint.Controllers;
using Sprint.Enums;
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
            GetUserAsyncReturns = new User { Id = 1 };

            // Act
            var result = await ControllerSUT.Index();

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<IEnumerable<UserGameWishlist>>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Add_ReturnsViewResult()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1 };

            // Act
            var result = ControllerSUT.Add("url");

            // Assert
            var viewResult = Assert.IsAssignableFrom<RedirectResult>(result);

            Assert.Null(viewResult);
        }

        [Fact]
        public async Task Add_ReturnsRedirectToActionResult_WhenGameIsAdded()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1 };

            // Act
            var result = await ControllerSUT.Add(1, "url");

            // Assert
            var wishlistItem = Assert.IsAssignableFrom<UserGameWishlist>(_context.Users.FirstOrDefault(g => g.Id == 1));
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);

            Assert.Equal(nameof(WishlistController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WhenGameIdIsFound()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1 };

            // Act
            var result = await ControllerSUT.Edit(WishlistVisibility.OnlyMe);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var selectList = Assert.IsAssignableFrom<SelectList>(viewResult.ViewData["Wishlist"]);

            Assert.NotEmpty(selectList);
        }

        [Theory]
        [InlineData(WishlistVisibility.Everyone)]
        [InlineData(WishlistVisibility.OnlyMe)]
        [InlineData(WishlistVisibility.FriendsOnly)]
        public async Task Edit_ReturnsNotFound_WhenGameIdIsNotFound(WishlistVisibility wishlistVisibility)
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1 };

            // Act
            var result = await ControllerSUT.Edit(wishlistVisibility);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Remove_ReturnsRedirectToActionResult_WhenGameIsRemoved()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1 };

            // Act
            var result = await ControllerSUT.Remove(1, "url");

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Null(_context.Games.FirstOrDefault(g => g.GameId == 1));
            Assert.Equal(nameof(WishlistController.Index), redirectResult.ActionName);
        }

        [Theory]
        [InlineData(null, "")]
        [InlineData(666, "url")]
        public async Task Remove_ReturnsNotFound_WhenGameIdIsNotFound(int? gameId, string url)
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1 };

            // Act
            var result = await ControllerSUT.Remove((int)gameId, url);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }
    }
}
