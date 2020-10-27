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
    public class GameControllerTests : DBContextController<GameController>
    {
        public override GameController CreateControllerSUT()
        {
            return new GameController(_mockUserManager.Object, _context);
        }

        [Fact]
        public async Task Index_ReturnsViewResult()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Index(null, null, null);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Game>>(viewResult.ViewData.Model);
            Assert.NotEmpty(model);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WhenGameIdIsFound()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Details(1);

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

            // Act
            var result = await ControllerSUT.Details(gameId);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public void Create_ReturnsViewResult()
        {
            // Arrange
            
            // Act
            var result = ControllerSUT.Create();
            
            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);

            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Create_ReturnsRedirectToActionResult_WhenGameIsCreated()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Create(new Game { Developer = "TEST_DEVELOPER", Name = "NEW_GAME", GameTypeId = 2 });

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

            // Act
            var result = await ControllerSUT.Edit(1);

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

            // Act
            var result = await ControllerSUT.Edit(gameId);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(666)]
        public async Task Edit_ReturnsNotFound_WhenGameIdDoesNotMatchPostBody(int gameId)
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Edit(2, new Game { GameId = gameId, Developer = "Blizzard", Name = "StarCraft", GameTypeId = 2 });

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsRedirectToActionResult_WhenGameIsUpdated()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Edit(1, new Game { GameId = 1, Developer = "Blizzard", Name = "StarCraft", GameTypeId = 2 });

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

            // Act
            var result = await ControllerSUT.Delete(1);

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

            // Act
            var result = await ControllerSUT.Delete(gameId);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_ReturnsRedirectToActionResult_WhenGameIsDeleted()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.DeleteConfirmed(1);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Null(_context.Games.FirstOrDefault(g => g.GameId == 1));
            Assert.Equal(nameof(GameController.Index), redirectResult.ActionName);
        }
    }
}
