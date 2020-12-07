using Microsoft.AspNetCore.Mvc;
using Sprint.Controllers;
using Sprint.Enums;
using Sprint.Models;
using Sprint.Tests.Helpers;
using Sprint.ViewModels;
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
            var wishlistItem = Assert.IsAssignableFrom<WishlistViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task UserWishList_ReturnsViewResult()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1, UserName = "bob" };

            // Act
            var result = await ControllerSUT.Shared("bob");

            // Assert
            var viewResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(nameof(WishlistController.Index), viewResult.ActionName);
        }

        [Fact]
        public async Task UserWishList_NotFoundResult()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1, UserName = "bob" };

            // Act
            var result = await ControllerSUT.Shared("fred");

            // Assert
            var viewResult = Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task UserWishList_RedirectToActionResult()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1, UserName = "bob" };
            FindByNameAsyncReturns = new User { Id = 2, UserName = "fred", WishlistVisibility = WishlistVisibility.OnlyMe };

            // Act
            var result = await ControllerSUT.Shared("fred");

            // Assert
            var viewResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(nameof(FriendsController.Index), viewResult.ActionName);
            Assert.Equal("Friends", viewResult.ControllerName);
        }

        [Fact]
        public async Task UserWishList_RedirectToActionResult_WhenFriendshipIsPending()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1, UserName = "bob" };
            FindByNameAsyncReturns = new User { Id = 2, UserName = "fred", WishlistVisibility = WishlistVisibility.FriendsOnly };

            // Act
            var result = await ControllerSUT.Shared("fred");

            // Assert
            var viewResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(nameof(FriendsController.Index), viewResult.ActionName);
            Assert.Equal("Friends", viewResult.ControllerName);
        }

        [Fact]
        public async Task UserWishList_ReturnsViewResult_WhenSuccessful()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1, UserName = "bob" };
            FindByNameAsyncReturns = new User { Id = 2, UserName = "fred", WishlistVisibility = WishlistVisibility.Everyone };

            // Act
            var result = await ControllerSUT.Shared("fred");

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var wishlistItem = Assert.IsAssignableFrom<WishlistViewModel>(viewResult.ViewData.Model);
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
        }

        [Fact]
        public async Task Add_ReturnsRedirectResult_WhenGameIsAdded()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1 };

            // Act
            var result = await ControllerSUT.Add(1, "url");

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectResult>(result);
        }

        [Fact]
        public async Task Add_ReturnsRedirectToActionResult_WhenGameIsAdded()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1 };

            // Act
            var result = await ControllerSUT.Add(1, null);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);

            Assert.Equal(nameof(GameController.Index), redirectResult.ActionName);
            Assert.Equal("Game", redirectResult.ControllerName);
        }

        [Fact]
        public async Task Edit_RedirectToAction_WhenGameIdIsFound()
        {
            // Arrange
            GetUserAsyncReturns = _context.Users.FirstOrDefault();

            // Act
            var result = await ControllerSUT.Edit(WishlistVisibility.OnlyMe);

            // Assert
            var viewResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(nameof(GameController.Index), viewResult.ActionName);
        }

        [Fact]
        public async Task Remove_ReturnsRedirectResult_WhenGameIsRemoved()
        {
            // Arrange
            User user = _context.Users.FirstOrDefault();
            Game game = _context.Games.FirstOrDefault();
            GetUserAsyncReturns = _context.Users.FirstOrDefault();
            _context.UserGameWishlists.Add(new UserGameWishlist {
                UserId = user.Id,
                GameId = game.GameId
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Remove(1, "url");

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectResult>(result);
        }

        [Fact]
        public async Task Remove_ReturnsRedirectToActionResult_WhenGameIsRemoved()
        {
            // Arrange
            User user = _context.Users.FirstOrDefault();
            Game game = _context.Games.FirstOrDefault();
            GetUserAsyncReturns = _context.Users.FirstOrDefault();
            _context.UserGameWishlists.Add(new UserGameWishlist
            {
                UserId = user.Id,
                GameId = game.GameId
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Remove(1, null);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Theory]
        [InlineData(666, "url")]
        public async Task Remove_ReturnsNotFound_WhenGameIdIsNotFound(int gameId, string url)
        {
            // Arrange
            GetUserAsyncReturns = _context.Users.FirstOrDefault();

            // Act
            var result = await ControllerSUT.Remove(gameId, url);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public void Remove_ReturnsRedirectResult_WhenNullOrEmpty()
        {
            // Arrange
            GetUserAsyncReturns = _context.Users.FirstOrDefault();

            // Act
            var result = ControllerSUT.Remove("bob");

            // Assert
            Assert.IsAssignableFrom<RedirectResult>(result);
        }

        [Fact]
        public void Remove_ReturnsRedirectToActionResult_WhenNullOrEmpty()
        {
            // Arrange
            GetUserAsyncReturns = _context.Users.FirstOrDefault();

            // Act
            var result = ControllerSUT.Remove(null);

            // Assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }
    }
}
