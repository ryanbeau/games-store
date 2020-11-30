using Microsoft.AspNetCore.Mvc;
using Sprint.Controllers;
using Sprint.Models;
using Sprint.Tests.Helpers;
using Sprint.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
        private Wallet otherUserWallet;
        private Address otherUserAddress;

        private const int IDENTITY_USER_ID = 1;
        private const int OTHER_USER_WALLET_ID = 1;
        private const int OTHER_USER_ADDRESS_ID = 1;
        private const decimal GAME1_PRICE = 11.12m;
        private const decimal GAME2_PRICE = 12.34m;

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
                identityUser = CreateUser(IDENTITY_USER_ID, IdentityPrincipalUserName, "email1@email.com", true, "Password0!", "Name1", "human", "555-555-5555", new DateTime(1970, 01, 01)),
                recipientUser = CreateUser(2, "UserName2", "email3@email.com", true, "Password0!", "Name2", "human", "555-555-5555", new DateTime(1970, 01, 01)),
                // game type
                testGameType = CreateGameType(1, "TestGameType"),
                // game
                game1 = CreateGame(1, "Game<1>", "Dev", GAME1_PRICE, testGameType.GameTypeId),
                game2 = CreateGame(2, "Game2", "Dev", 999.99m, testGameType.GameTypeId),
                // discount
                CreateDiscount(default, GAME2_PRICE, game2.GameId, DateTime.Now.Subtract(TimeSpan.FromHours(1)), DateTime.Now.Add(TimeSpan.FromHours(1))),
                // wallet
                otherUserWallet = CreateWallet(OTHER_USER_WALLET_ID, recipientUser.Id, "1234567890123456", "25", "05"),
                // address
                otherUserAddress = CreateAddress(OTHER_USER_ADDRESS_ID, recipientUser.Id, "street", "city", "province", "postal")
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
            Assert.Equal(GAME1_PRICE + GAME2_PRICE, model.ItemsTotalPrice);
            Assert.Equal(CheckoutController.TAX_PERCENT, model.TaxPercent);
            Assert.Equal(CheckoutController.INDIVIDUAL_SHIPPING_COST, model.IndividualShippingCost);
            Assert.Single(model.Addresses);
            Assert.Single(model.WalletCreditCards);
            Assert.Equal(address, model.Addresses.First());
            Assert.Equal(wallet, model.WalletCreditCards.First());
        }

        [Theory]
        [InlineData(OTHER_USER_WALLET_ID)] //refer to another user's Wallet
        [InlineData(null)]
        public async Task Order_ReturnsRedirectToActionResult_WhenWalletIsNull(int? walletId)
        {
            // Arrange
            var address = CreateAddress(default, identityUser.Id, "street", "city", "province", "postal");
            var wallet = CreateWallet(default, identityUser.Id, "1234567890123456", "25", "05");
            var cartGame = CreateCartGame(default, identityUser, recipientUser, game1);

            GetUserAsyncReturns = identityUser;

            _context.CartGames.Add(cartGame);
            _context.Address.Add(address);
            _context.Wallet.Add(wallet);

            await _context.SaveChangesAsync();

            var orderDetails = new OrderDetails()
            {
                OrderItems = new List<OrderDetailsItem>()
                {
                    new OrderDetailsItem { CartGameId = cartGame.CartGameId, ShipItem = false },
                },
                WalletId = walletId,
            };

            // Act
            var result = await ControllerSUT.Order(orderDetails);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            Assert.Equal("Payment type is required.", ControllerSUT.TempData["CheckoutAlert"]);
        }

        [Theory]
        [InlineData(OTHER_USER_ADDRESS_ID)] //refer to another user's Wallet
        [InlineData(null)]
        public async Task Order_ReturnsRedirectToActionResult_WhenShippingItemsButNoShippingAddress(int? shippingAddressId)
        {
            // Arrange
            var address = CreateAddress(default, identityUser.Id, "street", "city", "province", "postal");
            var wallet = CreateWallet(default, identityUser.Id, "1234567890123456", "25", "05");
            var cartGame = CreateCartGame(default, identityUser, recipientUser, game1);

            GetUserAsyncReturns = identityUser;

            _context.CartGames.Add(cartGame);
            _context.Address.Add(address);
            _context.Wallet.Add(wallet);

            await _context.SaveChangesAsync();

            var orderDetails = new OrderDetails()
            {
                OrderItems = new List<OrderDetailsItem>()
                {
                    new OrderDetailsItem { CartGameId = cartGame.CartGameId, ShipItem = true }, // with shipping item
                },
                WalletId = wallet.WalletId,
                ShippingId = shippingAddressId,
            };

            // Act
            var result = await ControllerSUT.Order(orderDetails);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            Assert.Equal("Shipping address is required to ship a game.", ControllerSUT.TempData["CheckoutAlert"]);
        }

        [Theory]
        [InlineData(null, OTHER_USER_ADDRESS_ID)] //refer to another user's Address
        [InlineData(false, OTHER_USER_ADDRESS_ID)] //refer to another user's Address
        [InlineData(null, null)]
        [InlineData(false, null)]
        public async Task Order_ReturnsRedirectToActionResult_WhenShippingItemsButBillingAddressIsInvalid(bool? sameAsShippingAddress, int? billingAddressId)
        {
            // Arrange
            var address = CreateAddress(default, identityUser.Id, "street", "city", "province", "postal");
            var wallet = CreateWallet(default, identityUser.Id, "1234567890123456", "25", "05");
            var cartGame = CreateCartGame(default, identityUser, recipientUser, game1);

            GetUserAsyncReturns = identityUser;

            _context.CartGames.Add(cartGame);
            _context.Address.Add(address);
            _context.Wallet.Add(wallet);

            await _context.SaveChangesAsync();

            var orderDetails = new OrderDetails()
            {
                OrderItems = new List<OrderDetailsItem>()
                {
                    new OrderDetailsItem { CartGameId = cartGame.CartGameId, ShipItem = true }, // with shipping item
                },
                WalletId = wallet.WalletId,
                ShippingId = address.AddressId,
                SameAsShippingAddress = sameAsShippingAddress,
                BillingId = billingAddressId,
            };

            // Act
            var result = await ControllerSUT.Order(orderDetails);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            Assert.Equal("Billing address is required.", ControllerSUT.TempData["CheckoutAlert"]);
        }

        [Theory]
        [InlineData(-500.0)]
        [InlineData(0.0)]
        [InlineData(12345.0)]
        public async Task Order_ReturnsRedirectToActionResult_WhenItemsTotalPriceDiffersFromExpected(decimal itemsTotalPrice)
        {
            // Arrange
            var address = CreateAddress(default, identityUser.Id, "street", "city", "province", "postal");
            var wallet = CreateWallet(default, identityUser.Id, "1234567890123456", "25", "05");
            var cartGame = CreateCartGame(default, identityUser, recipientUser, game1);

            GetUserAsyncReturns = identityUser;

            _context.CartGames.Add(cartGame);
            _context.Address.Add(address);
            _context.Wallet.Add(wallet);

            await _context.SaveChangesAsync();

            var orderDetails = new OrderDetails()
            {
                OrderItems = new List<OrderDetailsItem>()
                {
                    new OrderDetailsItem { CartGameId = cartGame.CartGameId, ShipItem = false },
                },
                WalletId = wallet.WalletId,
                ShippingId = address.AddressId,
                ItemsTotalPrice = itemsTotalPrice,
            };

            // Act
            var result = await ControllerSUT.Order(orderDetails);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            Assert.Equal("Checkout price currently differs from original. Please reconfirm order.", ControllerSUT.TempData["CheckoutAlert"]);
        }

        [Fact]
        public async Task Order_ReturnsRedirectToActionResult_WhenOrderIsSuccessful()
        {
            // Arrange
            var address = CreateAddress(default, identityUser.Id, "street", "city", "province", "postal");
            var wallet = CreateWallet(default, identityUser.Id, "1234567890123456", "25", "05");
            var cartGame1 = CreateCartGame(default, identityUser, recipientUser, game1);
            var cartGame2 = CreateCartGame(default, identityUser, identityUser, game2);

            GetUserAsyncReturns = identityUser;

            _context.CartGames.Add(cartGame1);
            _context.CartGames.Add(cartGame2);
            _context.Address.Add(address);
            _context.Wallet.Add(wallet);

            await _context.SaveChangesAsync();

            var orderDetails = new OrderDetails()
            {
                OrderItems = new List<OrderDetailsItem>()
                {
                    new OrderDetailsItem { CartGameId = cartGame1.CartGameId, ShipItem = false },
                    new OrderDetailsItem { CartGameId = cartGame2.CartGameId, ShipItem = false },
                },
                WalletId = wallet.WalletId,
                ShippingId = address.AddressId,
                ItemsTotalPrice = GAME1_PRICE + GAME2_PRICE,
            };

            // Act
            var result = await ControllerSUT.Order(orderDetails);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal("Confirmation", redirectResult.ActionName);
            Assert.Equal("Your order has been successfully processed.", ControllerSUT.TempData["CheckoutSuccess"]);
            Assert.Empty(_context.CartGames.Where(c => c.CartUserId == identityUser.Id));
            Assert.Single(_context.Orders.Where(c => c.UserId == identityUser.Id));
            Assert.Single(_context.OrderItems.Where(c => c.OwnerUserId == identityUser.Id));
            Assert.Single(_context.OrderItems.Where(c => c.OwnerUserId == recipientUser.Id));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("0e64e3d2-8757-4638-89ce-afb18db87fe5")]
        public async Task Confirmation_ReturnsNotFoundResult_WhenOrderIsNotFound(string orderNumber)
        {
            // Arrange
            GetUserAsyncReturns = identityUser;

            var order1 = new Order { UserId = recipientUser.Id,  WalletId = otherUserWallet.WalletId, OrderNumber = "0e64e3d2-8757-4638-89ce-afb18db87fe5"};
            _context.Orders.Add(order1);
            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Confirmation(orderNumber);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Confirmation_ReturnsViewResult_WhenSuccessful()
        {
            // Arrange
            GetUserAsyncReturns = identityUser;

            var wallet = CreateWallet(default, identityUser.Id, "1234567890123456", "25", "05");
            _context.Wallet.Add(wallet);
            await _context.SaveChangesAsync();

            var order1 = new Order { UserId = identityUser.Id, WalletId = wallet.WalletId, OrderNumber = "0e64e3d2-8757-4638-89ce-afb18db87fe5" };
            _context.Orders.Add(order1);
            await _context.SaveChangesAsync();

            var orderItem1 = new OrderItem { OrderId = order1.OrderId, OwnerUserId = identityUser.Id, GameId = game1.GameId };
            _context.OrderItems.Add(orderItem1);
            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Confirmation("0e64e3d2-8757-4638-89ce-afb18db87fe5");

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ConfirmationItemViewModel>>(viewResult.Model);
            Assert.Single(model);
        }

        [Theory]
        [InlineData(null, IDENTITY_USER_ID, false)]
        [InlineData("otherUserOrderItem", IDENTITY_USER_ID, true)]
        [InlineData("0e64e3d2-8757-4638-89ce-afb18db87fe5", IDENTITY_USER_ID, true)]
        public async Task Download_ReturnsNotFoundResult_WhenIdIsNullOrOrderItemNotFound(string id, int userId, bool physicallyOwned)
        {
            // Arrange
            GetUserAsyncReturns = identityUser;

            var wallet = CreateWallet(default, identityUser.Id, "1234567890123456", "25", "05");
            _context.Wallet.Add(wallet);
            await _context.SaveChangesAsync();

            var order1 = new Order { UserId = identityUser.Id, WalletId = wallet.WalletId, OrderNumber = "OrderNumber1" };
            var order2 = new Order { UserId = recipientUser.Id, WalletId = otherUserWallet.WalletId, OrderNumber = "OrderNumber2" };
            _context.Orders.AddRange(order1, order2);
            await _context.SaveChangesAsync();

            var orderItem1 = new OrderItem { OrderId = order1.OrderId, OwnerUserId = userId, GameId = game1.GameId, PhysicallyOwned = physicallyOwned, ItemNumber = "0e64e3d2-8757-4638-89ce-afb18db87fe5" };
            var orderItem2 = new OrderItem { OrderId = order1.OrderId, OwnerUserId = recipientUser.Id, GameId = game1.GameId, PhysicallyOwned = physicallyOwned, ItemNumber = "otherUserOrderItem" };
            _context.OrderItems.AddRange(orderItem1, orderItem2);
            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Download(id);

            // Assert
            var viewResult = Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Download_ReturnsPhysicalFileResult_WhenSuccessful()
        {
            // Arrange
            var fileName = Path.GetFullPath("./File/static-game-asset.png");
            var downloadName = $"Game1.png";

            GetUserAsyncReturns = identityUser;

            var wallet = CreateWallet(default, identityUser.Id, "1234567890123456", "25", "05");
            _context.Wallet.Add(wallet);
            await _context.SaveChangesAsync();

            var order1 = new Order { UserId = identityUser.Id, WalletId = wallet.WalletId, OrderNumber = "OrderNumber1" };
            _context.Orders.Add(order1);
            await _context.SaveChangesAsync();

            var orderItem1 = new OrderItem { OrderId = order1.OrderId, OwnerUserId = identityUser.Id, GameId = game1.GameId, PhysicallyOwned = false, ItemNumber = "0e64e3d2-8757-4638-89ce-afb18db87fe5" };
            _context.OrderItems.Add(orderItem1);
            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Download("0e64e3d2-8757-4638-89ce-afb18db87fe5");

            // Assert
            var fileResult = Assert.IsAssignableFrom<PhysicalFileResult>(result);
            Assert.Equal("image/png", fileResult.ContentType);
            Assert.Equal(fileName, fileResult.FileName);
            Assert.Equal(downloadName, fileResult.FileDownloadName);
        }
    }
}
