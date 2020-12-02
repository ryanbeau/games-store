using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sprint.Controllers;
using Sprint.Models;
using Sprint.Tests.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Sprint.Tests.Controllers
{
    public class ReviewControllerTests : DBContextController<ReviewsController>
    {
        private User identityUser;
        private User user1;
        private Game game1;
        private Review review1;

        protected override bool SeedWithApplicationData => false;

        public override ReviewsController CreateControllerSUT()
        {
            return new ReviewsController(_context, _mockUserManager.Object);
        }

        public override void SeedDatabase()
        {
            GameType testGameType;

            _context.AddRange(
                // identity user
                identityUser = CreateUser(2, IdentityPrincipalUserName, "email@email.com", true, "Password0!", "Name1", "human", "555-555-5555", new DateTime(1970, 01, 01)),
                // user
                user1 = CreateUser(1, IdentityPrincipalUserName, "email1@email.com", true, "Password0!", "Name1", "human", "555-555-5555", new DateTime(1970, 01, 01)),
                // game type
                testGameType = CreateGameType(1, "TestGameType"),
                // game
                game1 = CreateGame(1, "Game1", "Dev", 11.12m, testGameType.GameTypeId),
                review1 = CreateReview(1, user1.Id, game1.GameId, "Fun", 3)
            );

            _context.SaveChanges();
        }

        [Fact]
        public async Task Index_ReturnsViewResult()
        {
            // Arrange
            GetUserAsyncReturns = user1;

            // Act
            var result = await ControllerSUT.Index(game1.GameId);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var game = Assert.IsAssignableFrom<Game>(viewResult.ViewData.Model);
            Assert.NotEmpty(game.Reviews);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WhenIdIsFound()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Details(1);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<Review>(viewResult.ViewData.Model);
        }

        [Theory]
        [InlineData(1)]
        public async Task Create_ReturnsViewResult(int? gameId)
        {
            // Arrange
            GetUserAsyncReturns = identityUser;

            // Act
            var result = await ControllerSUT.Create(gameId, review1);

            // Assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsNotFoundResult_WhenGameIdNull()
        {
            // Arrange
            GetUserAsyncReturns = identityUser;

            // Act
            var result = await ControllerSUT.Create(null, review1);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsNotFoundResult_WhenUserIdNotFound()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Create(1, review1);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WhenReviewIdIsFound()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Edit(1);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<Review>(viewResult.ViewData.Model);

            Assert.NotNull(viewResult);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(666)]
        public async Task Edit_ReturnsNotFound_WhenReviewIdIsNotFound(int? reviewId)
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Edit(reviewId);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(666)]
        public async Task Edit_ReturnsNotFound_WhenIdDoesNotMatchPostBody(int gameId)
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Edit(2, new Review { ReviewId = review1.ReviewId, Game = game1, GameId = gameId, ReviewContent = "fun", Rating = 2 });

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        //[Fact]
        //public async Task Edit_ReturnsViewResult_WhenReviewIsUpdated()
        //{
        //    // Arrange

        //    // Act
        //    var result = await ControllerSUT.Edit(666, new Review { ReviewId = 666, UserId = user1.Id, GameId = 1, ReviewContent = "fun", Rating = 2 });

        //    // Assert
        //    var redirectResult = Assert.IsAssignableFrom<ViewResult>(result);
        //    var review = Assert.IsAssignableFrom<Review>(_context.Games.FirstOrDefault(g => g.GameId == 1).Reviews.FirstOrDefault(g => g.GameId == 1));

        //    Assert.Equal(1, review.GameId);
        //    Assert.Equal(1, review.UserId);
        //    Assert.Equal(1, review.ReviewId);
        //}

        [Fact]
        public async Task Delete_ReturnsViewResult_WhenGameIdIsFound()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Delete(1);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<Review>(viewResult.ViewData.Model);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(666)]
        public async Task Delete_ReturnsNotFound_WhenGameIdIsNotFound(int? reviewId)
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Delete(reviewId);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsRedirectToActionResult_WhenSuccess()
        {
            // Arrange
            GetUserAsyncReturns = user1;

            // Act
            var result = await ControllerSUT.Delete(review1.ReviewId);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<Review>(viewResult.ViewData.Model);
        }


        [Fact]
        public async Task DeleteConfirmed_ReturnsRedirectToActionResult_WhenReviewIsDeleted()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.DeleteConfirmed(1);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Null(_context.Reviews.FirstOrDefault(g => g.ReviewId == 1));
            Assert.Equal(nameof(ReviewsController.Index), redirectResult.ActionName);
        }
    }

}
