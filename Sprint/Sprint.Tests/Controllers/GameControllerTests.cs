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
        public async Task Index_ReturnsViewResult()
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
        public async Task Details_ReturnsViewResult_WhenGameIdIsFound()
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
        public async Task Details_ReturnsNotFound_WhenGameIdIsNotFound(int? gameId)
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
        public void Create_ReturnsViewResult()
        {
            // Arrange
            using var context = new GameStoreContext();
            GameController gameController = new GameController(context);
            
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
            using var context = new GameStoreContext(ContextOptions);
            GameController gameController = new GameController(context);

            // Act
            var result = await gameController.Create(new Game { Developer = "Blizzard", GameName = "StarCraft", GameTypeId = 2, Rating = 4 });

            // Assert
            var game = Assert.IsAssignableFrom<Game>(context.Game.FirstOrDefault(g => g.GameId == 2));
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);

            Assert.Equal(2, game.GameId);
            Assert.Equal("Blizzard", game.Developer);
            Assert.Equal("StarCraft", game.GameName);
            Assert.Equal(2, game.GameTypeId);
            Assert.Equal(4, game.Rating);
            Assert.Equal(nameof(GameController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WhenGameIdIsFound()
        {
            // Arrange
            using var context = new GameStoreContext(ContextOptions);
            GameController gameController = new GameController(context);

            // Act
            var result = await gameController.Edit(1);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var game = Assert.IsAssignableFrom<Game>(viewResult.ViewData.Model);
            var selectList = Assert.IsAssignableFrom<SelectList>(viewResult.ViewData["GameTypeId"]);

            Assert.Equal(2, selectList.Count());
            Assert.Equal(1, game.GameId);
            Assert.Equal("Valve", game.Developer);
            Assert.Equal("Half-life", game.GameName);
            Assert.Equal(1, game.GameTypeId);
            Assert.Equal(3, game.Rating);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(666)]
        public async Task Edit_ReturnsNotFound_WhenGameIdIsNotFound(int? gameId)
        {
            // Arrange
            using var context = new GameStoreContext(ContextOptions);
            GameController gameController = new GameController(context);

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
            using var context = new GameStoreContext(ContextOptions);
            GameController gameController = new GameController(context);

            // Act
            var result = await gameController.Edit(2, new Game { GameId = gameId, Developer = "Blizzard", GameName = "StarCraft", GameTypeId = 2, Rating = 4 });

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsRedirectToActionResult_WhenGameIsUpdated()
        {
            // Arrange
            using var context = new GameStoreContext(ContextOptions);
            GameController gameController = new GameController(context);

            // Act
            var result = await gameController.Edit(1, new Game { GameId = 1, Developer = "Blizzard", GameName = "StarCraft", GameTypeId = 2, Rating = 4 });

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            var game = Assert.IsAssignableFrom<Game>(context.Game.FirstOrDefault(g => g.GameId == 1));

            Assert.Equal(1, game.GameId);
            Assert.Equal("Blizzard", game.Developer);
            Assert.Equal("StarCraft", game.GameName);
            Assert.Equal(2, game.GameTypeId);
            Assert.Equal(4, game.Rating);
            Assert.Equal(nameof(GameController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task Delete_ReturnsViewResult_WhenGameIdIsFound()
        {
            // Arrange
            using var context = new GameStoreContext(ContextOptions);
            GameController gameController = new GameController(context);

            // Act
            var result = await gameController.Delete(1);

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
        public async Task Delete_ReturnsNotFound_WhenGameIdIsNotFound(int? gameId)
        {
            // Arrange
            using var context = new GameStoreContext(ContextOptions);
            GameController gameController = new GameController(context);

            // Act
            var result = await gameController.Delete(gameId);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_ReturnsRedirectToActionResult_WhenGameIsDeleted()
        {
            // Arrange
            using var context = new GameStoreContext(ContextOptions);
            GameController gameController = new GameController(context);

            // Act
            var result = await gameController.DeleteConfirmed(1);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Null(context.Game.FirstOrDefault(g => g.GameId == 1));
            Assert.Equal(nameof(GameController.Index), redirectResult.ActionName);
        }
    }
}
