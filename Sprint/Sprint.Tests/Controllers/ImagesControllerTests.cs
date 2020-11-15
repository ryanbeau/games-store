using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sprint.Controllers;
using Sprint.Models;
using Sprint.Tests.Helpers;
using Sprint.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
namespace Sprint.Tests.Controllers
{
    public class ImagesControllerTests : DBContextController<ImagesController>
    {
        public override ImagesController CreateControllerSUT()
        {
            return new ImagesController(_context);
        }

        //[Fact]
        //public async Task Index_ReturnsViewResult()
        //{
        //    // Arrange

        //    // Act
        //    var result = await ControllerSUT.Index(null);

        //    // Assert
        //    var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
        //}

        [Theory]
        [InlineData(null)]
        [InlineData(666)]
        public async Task Details_ReturnsNotFound_WhenIdIsNotFound(int? id)
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Details(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WhenGameIdIsFound()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Details(1);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<GameImage>(viewResult.ViewData.Model);
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
        public async Task Create_ReturnsRedirectToActionResult_WhenGameImageIsCreated()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Create(new GameImage { ImageType = (Enums.ImageType)3, ImageURL = "https://cdn.steamgriddb.com/thumb/df3cdfd672004f1a0058d81c56e7270a.png" });

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(nameof(ImagesController.Index), redirectResult.ActionName);
        }

        //[Fact]
        //public async Task Edit_ReturnsViewResult_WhenIdIsFound()
        //{
        //    // Arrange

        //    // Act
        //    var result = await ControllerSUT.Edit(1);

        //    // Assert
        //    var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
        //    var selectList = Assert.IsAssignableFrom<SelectList>(viewResult.ViewData["ImageType"]);
        //    Assert.IsAssignableFrom<GameImage>(viewResult.ViewData.Model);

        //    Assert.NotEmpty(selectList);
        //}

        [Theory]
        [InlineData(null)]
        [InlineData(666)]
        public async Task Edit_ReturnsNotFound_WhenImageIdIsNotFound(int? id)
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Edit(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(666)]
        public async Task Edit_ReturnsNotFound_WhenImageIdDoesNotMatchPostBody(int id)
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Edit(2, new GameImage {GameImageId=3, GameId =6, ImageType = (Enums.ImageType)2, ImageURL = "https://cdn.steamgriddb.com/thumb/df3cdfd672004f1a0058d81c56e7270a.png" });

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        //[Fact]
        //public async Task Edit_ReturnsViewREsult_WhenGameIsUpdated()
        //{
        //    // Arrange

        //    // Act
        //    var result = await ControllerSUT.Edit(1, new GameImage { GameImageId = 1, GameId = 6, ImageType = (Enums.ImageType)2, ImageURL = "https://cdn.steamgriddb.com/thumb/df3cdfd672004f1a0058d81c56e7270a.png" });

        //    // Assert
        //    var redirectResult = Assert.IsAssignableFrom<ViewResult>(result);
        //    var gameImage = Assert.IsAssignableFrom<GameImage>(_context.Games.FirstOrDefault(g => g.GameId == 6));

        //    Assert.Equal(6, gameImage.GameId);
        //    Assert.Equal("https://cdn.steamgriddb.com/thumb/df3cdfd672004f1a0058d81c56e7270a.png", gameImage.ImageURL);
        //    Assert.Equal(1, gameImage.GameImageId);
        //    Assert.Equal((Enums.ImageType)2, gameImage.ImageType);
        //}

        [Fact]
        public async Task Delete_ReturnsViewResult_WhenImageIdIsFound()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Delete(1);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<GameImage>(viewResult.ViewData.Model);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(666)]
        public async Task Delete_ReturnsNotFound_WhenImageIdIsNotFound(int? id)
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Delete(id);

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
            Assert.Equal(nameof(ImagesController.Index), redirectResult.ActionName);
        }




    }
}
