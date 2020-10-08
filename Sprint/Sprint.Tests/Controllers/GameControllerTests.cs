using Microsoft.AspNetCore.Mvc;
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
        protected override void Seed()
        {
            using var context = new GameStoreContext(ContextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.GameType.Add(new GameType { GameTypeId = 1, Type = "1st Person Shooter" });
            context.GameType.Add(new GameType { GameTypeId = 2, Type = "RTS" });
            context.Game.Add(new Game { GameId = 1, Developer = "Valve", GameName = "Half-life", GameTypeId = 1, Rating = 3 });

            context.SaveChanges();
        }

        [Fact]
        public async Task Test_Index_Returns_ViewResult()
        {
            // Arrange
            using var context = new GameStoreContext(ContextOptions);
            GameController gameController = new GameController(context);

            // Act
            var result = await gameController.Index();

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Game>>(viewResult.ViewData.Model);
            Assert.Single(model);

            var game = model.First();
            Assert.Equal(1, game.GameId);
            Assert.Equal("Valve", game.Developer);
            Assert.Equal("Half-life", game.GameName);
            Assert.Equal(1, game.GameTypeId);
            Assert.Equal(3, game.Rating);
        }

        [Fact]
        public async Task Test_Details_Returns_ViewResult()
        {
            // Arrange
            using var context = new GameStoreContext(ContextOptions);
            GameController gameController = new GameController(context);

            // Act
            var result = await gameController.Details(1);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var game = Assert.IsAssignableFrom<Game>(viewResult.ViewData.Model);

            Assert.Equal(1, game.GameId);
            Assert.Equal("Valve", game.Developer);
            Assert.Equal("Half-life", game.GameName);
            Assert.Equal(1, game.GameTypeId);
            Assert.Equal(3, game.Rating);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(666)]
        public async Task Test_Details_Returns_NotFound(int? gameId)
        {
            // Arrange
            using var context = new GameStoreContext(ContextOptions);
            GameController gameController = new GameController(context);

            // Act
            var result = await gameController.Details(gameId);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public void Test_Create_ReturnsEmpty_ViewResult()
        {
            // Arrange
            using var context = new GameStoreContext(ContextOptions);
            GameController gameController = new GameController(context);

            // Act
            var result = gameController.Create();

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Test_Create_Returns_RedirectToActionResult()
        {
            // Arrange
            using var context = new GameStoreContext(ContextOptions);
            GameController gameController = new GameController(context);

            // Act
            var result = await gameController.Create(new Game { GameId = 2, Developer = "Blizzard", GameName = "StarCraft", GameTypeId = 2, Rating = 4 });

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(nameof(GameController.Index), redirectResult.ActionName);
        }
    }
}
