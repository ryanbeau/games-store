using Microsoft.AspNetCore.Mvc;
using Sprint.Controllers;
using Sprint.Enums;
using Sprint.Models;
using Sprint.Tests.Helpers;
using Sprint.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Sprint.Tests.Controllers
{
    public class WalletControllerTests : DBContextController<WalletController>
    {
        private User identityUser;
        User user1;
        Wallet wallet1;

        protected override bool SeedWithApplicationData => false;

        public override WalletController CreateControllerSUT()
        {
            return new WalletController(_context, _mockUserManager.Object);
        }

        public override void SeedDatabase()
        {
            _context.AddRange(
                identityUser = CreateUser(2, IdentityPrincipalUserName, "email1@email.com", true, "Password0!", "Name1", "human", "555-555-5555", new DateTime(1970, 01, 01)),
                user1 = CreateUser(1, IdentityPrincipalUserName, "email2@email.com", true, "Password0!", "Name1", "human", "555-555-5555", new DateTime(1970, 01, 01)),
                wallet1 = CreateWallet(1, user1.Id, 1, "1234123412341234", "22", "12")
                );

            _context.SaveChanges();
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
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WhenWalletIdIsFound()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Details(1);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<Wallet>(viewResult.ViewData.Model);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(666)]
        public async Task Details_ReturnsNotFound_WhenWalletIdIsNotFound(int? id)
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Details(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }
        

        [Fact]
        public async Task Create_ReturnsRedirectToActionResult_WhenWalletIsCreated()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Create(new Wallet {UserId = user1.Id, WalletId =17, CardNumber = "1616161616161616",Month ="02", Year ="21"});

            // Assert
            var wallet = Assert.IsAssignableFrom<Wallet>(_context.Wallet.FirstOrDefault(w => w.WalletId == 17));
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(1, wallet.UserId);
            Assert.Equal(17, wallet.WalletId);
            Assert.Equal("1616161616161616", wallet.CardNumber);
            Assert.Equal("02", wallet.Month);
            Assert.Equal("21", wallet.Year);
            Assert.Equal(nameof(WalletController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WhenIdFound()
        {
            // Arrange
            GetUserAsyncReturns = identityUser;
            //Act
            var result = await ControllerSUT.Edit(1);

            //Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);

            Assert.IsAssignableFrom<Wallet>(viewResult.ViewData.Model);

            Assert.NotNull(viewResult);

        }
        [Theory]
        [InlineData(null)]
        [InlineData(666)]
        public async Task Edit_ReturnsNotFound_WhenWalletIdIsNotFound(int? walletId)
        {
            // Arrange
            GetUserAsyncReturns = identityUser;
            // Act
            var result = await ControllerSUT.Edit(walletId);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsViewResult_WhenWalletIsFound()
        {
            // Arrange
            GetUserAsyncReturns = identityUser;
            // Act
            var result = await ControllerSUT.Delete(1);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<Wallet>(viewResult.ViewData.Model);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(666)]
        public async Task Delete_ReturnsNotFound_WhenWalletIsNotFound(int? walletId)
        {
            // Arrange
            GetUserAsyncReturns = identityUser;
            // Act
            var result = await ControllerSUT.Delete(walletId);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsRedirectToActionResult_WhenSuccess()
        {
            // Arrange
            GetUserAsyncReturns = user1;

            // Act
            var result = await ControllerSUT.Delete(wallet1.WalletId);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<Wallet>(viewResult.ViewData.Model);
        }


        [Fact]
        public async Task DeleteConfirmed_ReturnsRedirectToActionResult_WhenWalletIsDeleted()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.DeleteConfirmed(1);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Null(_context.Wallet.FirstOrDefault(g => g.WalletId == 1));
            Assert.Equal(nameof(WalletController.Index), redirectResult.ActionName);
        }
    }
}
