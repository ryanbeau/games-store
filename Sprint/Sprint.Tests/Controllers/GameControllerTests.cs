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
    public class GameControllerTests : DBContextController
    {
        [Fact]
        public async Task Index_ReturnsViewResult()
        {
            // Arrange
            GameController gameController = new GameController(_context);

            // Act
            var result = await gameController.Index();

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Game>>(viewResult.ViewData.Model);
            Assert.NotEmpty(model);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WhenGameIdIsFound()
        {
            // Arrange
            GameController gameController = new GameController(_context);

            // Act
            var result = await gameController.Details(1);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<Game>(viewResult.ViewData.Model);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(666)]
        public async Task Details_ReturnsNotFound_WhenGameIdIsNotFound(int? gameId)
        {
            // Arrange
            GameController gameController = new GameController(_context);

            // Act
            var result = await gameController.Details(gameId);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public void Create_ReturnsViewResult()
        {
            // Arrange
            GameController gameController = new GameController(_context);
            
            // Act
            var result = gameController.Create();
            
            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);

            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Create_ReturnsRedirectToActionResult_WhenGameIsCreated()
        {
            // Arrange
            GameController gameController = new GameController(_context);

            // Act
            var result = await gameController.Create(new Game { Developer = "TEST_DEVELOPER", Name = "NEW_GAME", GameTypeId = 2 });

            // Assert
            var game = Assert.IsAssignableFrom<Game>(_context.Games.FirstOrDefault(g => g.Name == "NEW_GAME"));
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);

            Assert.Equal("TEST_DEVELOPER", game.Developer);
            Assert.Equal(2, game.GameTypeId);
            Assert.Equal(nameof(GameController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WhenGameIdIsFound()
        {
            // Arrange
            GameController gameController = new GameController(_context);

            // Act
            var result = await gameController.Edit(1);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var selectList = Assert.IsAssignableFrom<SelectList>(viewResult.ViewData["GameTypeId"]);
            Assert.IsAssignableFrom<Game>(viewResult.ViewData.Model);

            Assert.NotEmpty(selectList);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(666)]
        public async Task Edit_ReturnsNotFound_WhenGameIdIsNotFound(int? gameId)
        {
            // Arrange
            GameController gameController = new GameController(_context);

            // Act
            var result = await gameController.Edit(gameId);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(666)]
        public async Task Edit_ReturnsNotFound_WhenGameIdDoesNotMatchPostBody(int gameId)
        {
            // Arrange
            GameController gameController = new GameController(_context);

            // Act
            var result = await gameController.Edit(2, new Game { GameId = gameId, Developer = "Blizzard", Name = "StarCraft", GameTypeId = 2 });

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsRedirectToActionResult_WhenGameIsUpdated()
        {
            // Arrange
            GameController gameController = new GameController(_context);

            // Act
            var result = await gameController.Edit(1, new Game { GameId = 1, Developer = "Blizzard", Name = "StarCraft", GameTypeId = 2 });

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            var game = Assert.IsAssignableFrom<Game>(_context.Games.FirstOrDefault(g => g.GameId == 1));

            Assert.Equal(1, game.GameId);
            Assert.Equal("Blizzard", game.Developer);
            Assert.Equal("StarCraft", game.Name);
            Assert.Equal(2, game.GameTypeId);
            Assert.Equal(nameof(GameController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task Delete_ReturnsViewResult_WhenGameIdIsFound()
        {
            // Arrange
            GameController gameController = new GameController(_context);

            // Act
            var result = await gameController.Delete(1);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<Game>(viewResult.ViewData.Model);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(666)]
        public async Task Delete_ReturnsNotFound_WhenGameIdIsNotFound(int? gameId)
        {
            // Arrange
            GameController gameController = new GameController(_context);

            // Act
            var result = await gameController.Delete(gameId);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_ReturnsRedirectToActionResult_WhenGameIsDeleted()
        {
            // Arrange
            GameController gameController = new GameController(_context);

            // Act
            var result = await gameController.DeleteConfirmed(1);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Null(_context.Games.FirstOrDefault(g => g.GameId == 1));
            Assert.Equal(nameof(GameController.Index), redirectResult.ActionName);
        }
    }
}
