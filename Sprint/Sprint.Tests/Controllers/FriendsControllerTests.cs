using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sprint.Controllers;
using Sprint.Models;
using Sprint.Tests.Helpers;
using Sprint.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Sprint.Tests.Controllers
{
    public class FriendsControllerTests : DBContextController<FriendsController>
    {
        public override FriendsController CreateControllerSUT()
        {
            return new FriendsController(_mockUserManager.Object, _context);
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
            var friends = Assert.IsAssignableFrom<FriendsViewModel>(viewResult.ViewData.Model);
            Assert.IsAssignableFrom<IEnumerable<UserRelationship>>(friends.FriendRelationships);
            Assert.IsAssignableFrom<IEnumerable<UserRelationship>>(friends.PendingRelationships);
        }

        [Fact]
        public void Add_ReturnsViewResult()
        {
            // Arrange

            // Act
            var result = ControllerSUT.Add();

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Theory]
        [InlineData("     ")]
        [InlineData("")]
        [InlineData(null)]
        public async Task AddUsername_ReturnsViewResult_WithModelError_WhenUsernamIsNullOrEmpty(string username)
        {
            // Arrange
            FindByNameAsyncReturns = new User { Id = 1, UserName = "admin" };

            // Act
            var result = await ControllerSUT.AddUsername(username);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<ModelStateEntry>(ControllerSUT.ModelState["username"]);
        }

        [Fact]
        public async Task AddUsername_ReturnsViewResult_WithModelError_WhenUserIsNotFound()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 2, UserName = null };
            FindByNameAsyncReturns = null;

            // Act
            var result = await ControllerSUT.AddUsername("does not exist");

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<ModelStateEntry>(ControllerSUT.ModelState["username"]);
        }

        [Fact]
        public async Task AddUsername_ReturnsViewResult_WithModelError_WhenUserIsItself()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1, UserName = "User1" };
            FindByNameAsyncReturns = new User { Id = 1, UserName = "User1" };

            // Act
            var result = await ControllerSUT.AddUsername("testing username comparison on Identity User");

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<ModelStateEntry>(ControllerSUT.ModelState["username"]);
        }

        [Fact]
        public async Task AddUsername_ReturnsRedirectToActionResult_WhenRelatingExists()
        {
            // Arrange
            FindByNameAsyncReturns = new User { Id = 1, UserName = "admin" };
            GetUserAsyncReturns = new User { Id = 2, UserName = "user" };

            _context.UserRelationships.Add(new UserRelationship { RelatingUserId = 2, RelatedUserId = 1 });

            // Act
            await _context.SaveChangesAsync();

            var result = await ControllerSUT.AddUsername("admin");

            // Assert
            var redirectToActionResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.IsAssignableFrom<string>(ControllerSUT.TempData["FriendInvite"]);
        }

        [Fact]
        public async Task AddUsername_ReturnsRedirectToActionResult_WhenRelatedExists()
        {
            // Arrange
            FindByNameAsyncReturns = new User { Id = 1, UserName = "admin" };
            GetUserAsyncReturns = new User { Id = 2, UserName = "user" };

            _context.UserRelationships.Add(new UserRelationship { RelatingUserId = 1, RelatedUserId = 2 });

            // Act
            await _context.SaveChangesAsync();

            var result = await ControllerSUT.AddUsername("admin");

            // Assert
            var redirectToActionResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.IsAssignableFrom<string>(ControllerSUT.TempData["FriendInvite"]);
        }

        [Fact]
        public async Task AddUsername_ReturnsRedirectToActionResult_WhenRelatingIsAdded()
        {
            // Arrange
            FindByNameAsyncReturns = new User { Id = 1, UserName = "admin" };
            GetUserAsyncReturns = new User { Id = 2, UserName = "user" };

            // Act
            var result = await ControllerSUT.AddUsername("admin");

            // Assert
            var redirectToActionResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.IsAssignableFrom<string>(ControllerSUT.TempData["FriendInvite"]);
        }

        [Theory]
        [InlineData("     ")]
        [InlineData("")]
        [InlineData(null)]
        public async Task Remove_ReturnsNotFound_WhenUsernameIsNullOrEmpty(string username)
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Remove(username);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Remove_ReturnsBadRequest_WhenUserIsItself()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1, UserName = "me" };
            FindByNameAsyncReturns = new User { Id = 1, UserName = "me" };

            // Act
            var result = await ControllerSUT.Remove("me");

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Fact]
        public async Task Remove_ReturnsNotFound_WhenRelatedDoesNotExist()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 2, UserName = "user2" };
            FindByNameAsyncReturns = new User { Id = 1, UserName = "user1" };

            // add relationship in opposite order
            _context.UserRelationships.Add(new UserRelationship { RelatingUserId = 1, RelatedUserId = 2 });

            // Act
            await _context.SaveChangesAsync();

            var result = await ControllerSUT.Remove("user1");

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Remove_ReturnsViewResult_WhenRelationshipExists()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 2, UserName = "user2" };
            FindByNameAsyncReturns = new User { Id = 1, UserName = "user1" };

            _context.UserRelationships.Add(new UserRelationship { RelatingUserId = 2, RelatedUserId = 1 });

            // Act
            await _context.SaveChangesAsync();

            var result = await ControllerSUT.Remove("user1");

            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Theory]
        [InlineData("     ")]
        [InlineData("")]
        [InlineData(null)]
        public async Task RemoveConfirmed_ReturnsNotFound_WhenUsernameIsNullOrEmpty(string username)
        {
            // Arrange

            // Act
            var result = await ControllerSUT.RemoveConfirmed(username);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task RemoveConfirmed_ReturnsBadRequest_WhenUserIsItself()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1, UserName = "user1" };
            FindByNameAsyncReturns = new User { Id = 1, UserName = "user1" };

            // Act
            var result = await ControllerSUT.RemoveConfirmed("user1");

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Fact]
        public async Task RemoveConfirmed_ReturnsRedirectToAction_WhenRelationshipsAreRemoved()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1, UserName = "user1" };
            FindByNameAsyncReturns = new User { Id = 2, UserName = "user2" };

            _context.UserRelationships.Add(new UserRelationship { RelatingUserId = 1, RelatedUserId = 2 });
            _context.UserRelationships.Add(new UserRelationship { RelatingUserId = 2, RelatedUserId = 1 });

            // Act
            await _context.SaveChangesAsync();

            var result = await ControllerSUT.RemoveConfirmed("user1");

            // Assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }
    }
}
