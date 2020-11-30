using Microsoft.AspNetCore.Mvc;
using Sprint.Controllers;
using Sprint.Models;
using Sprint.Tests.Helpers;
using Sprint.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Sprint.Tests.Controllers
{
    public class CheckoutControllerTests : DBContextController<CheckoutController>
    {
        private User identityUser;
        private User recipientUser;
        private Game game1;
        private Game game2;

        protected override bool SeedWithApplicationData => false;

        public override CheckoutController CreateControllerSUT()
        {
            return new CheckoutController(_mockUserManager.Object, _context);
        }

        public override void SeedDatabase()
        {
            GameType testGameType;

            _context.AddRange(
                // user
                identityUser = CreateUser(1, IdentityPrincipalUserName, "email1@email.com", true, "Password0!", "Name1", "human", "555-555-5555", new DateTime(1970, 01, 01)),
                recipientUser = CreateUser(2, "UserName2", "email3@email.com", true, "Password0!", "Name2", "human", "555-555-5555", new DateTime(1970, 01, 01)),
                // game type
                testGameType = CreateGameType(1, "TestGameType"),
                // game
                game1 = CreateGame(1, "Game1", "Dev", 11.12m, testGameType.GameTypeId),
                game2 = CreateGame(2, "Game2", "Dev", 12.34m, testGameType.GameTypeId)
            );

            _context.SaveChanges();
        }

        [Fact]
        public async Task Checkout_ReturnsRedirectToActionResult_WhenCartItemsAreEmpty()
        {
            // Arrange
            GetUserAsyncReturns = identityUser;

            // Act
            var result = await ControllerSUT.Checkout();

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            Assert.Equal("Cart", redirectResult.ControllerName);
        }

        [Fact]
        public async Task Checkout_ReturnsViewResult_WhenSuccessful()
        {
            // Arrange
            var address = CreateAddress(default, identityUser.Id, "street", "city", "province", "postal");
            var wallet = CreateWallet(default, identityUser.Id, "1234567890123456", "25", "05");

            GetUserAsyncReturns = identityUser;

            // cart games
            _context.CartGames.AddRange(
                CreateCartGame(default, identityUser, recipientUser, game1),
                CreateCartGame(default, identityUser, identityUser, game2)
            );
            // addresses
            _context.Address.AddRange(
                CreateAddress(default, recipientUser.Id, "street", "city", "province", "postal"),
                address
            );
            // credit cards
            _context.Wallet.AddRange(
                CreateWallet(default, recipientUser.Id, "1234567890123456", "25", "05"),
                wallet
            );

            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Checkout();

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CheckoutViewModel>(viewResult.Model);
            Assert.Equal(23.46m, model.ItemsTotalPrice);
            Assert.Equal(CheckoutController.TAX_PERCENT, model.TaxPercent);
            Assert.Equal(CheckoutController.INDIVIDUAL_SHIPPING_COST, model.IndividualShippingCost);
            Assert.Single(model.Addresses);
            Assert.Single(model.WalletCreditCards);
            Assert.Equal(address, model.Addresses.First());
            Assert.Equal(wallet, model.WalletCreditCards.First());
        }
    }
}
