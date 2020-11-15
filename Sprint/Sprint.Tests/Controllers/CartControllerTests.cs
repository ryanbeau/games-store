using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sprint.Controllers;
using Sprint.Enums;
using Sprint.Models;
using Sprint.Tests.Helpers;
using Sprint.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Sprint.Tests.Controllers
{
    public class CartControllerTests : DBContextController<CartController>
    {
        private User identityUser;
        private User user2;
        private User user3;
        private Game game1;

        protected override bool SeedWithApplicationData => false;

        public override CartController CreateControllerSUT()
        {
            return new CartController(_mockUserManager.Object, _context);
        }

        public override void SeedDatabase()
        {
            GameType testGameType;

            _context.AddRange(
                // user
                identityUser = CreateUser(1, IdentityPrincipalUserName, "email1@email.com", true, "Password0!", "Name1", "human", "555-555-5555", new DateTime(1970, 01, 01)),
                user2 = CreateUser(2, "UserName2", "email3@email.com", true, "Password0!", "Name2", "human", "555-555-5555", new DateTime(1970, 01, 01)),
                user3 = CreateUser(3, "UserName3", "email4@email.com", true, "Password0!", "Name3", "human", "555-555-5555", new DateTime(1970, 01, 01)),
                // game type
                testGameType = CreateGameType(1, "TestGameType"),
                // game
                game1 = CreateGame(1, "Game1", "Dev", 11.12m, testGameType.GameTypeId)
            );

            _context.SaveChanges();
        }

        [Fact]
        public async Task Index_ReturnsViewResult()
        {
            // Arrange
            GetUserAsyncReturns = identityUser;

            // Act
            var result = await ControllerSUT.Index();

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var cart = Assert.IsAssignableFrom<CartViewModel>(viewResult.ViewData.Model);
            Assert.IsAssignableFrom<IEnumerable<CartItemViewModel>>(cart.Items);
            Assert.Equal(identityUser, cart.User);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(null)]
        public async Task Add_ReturnsNotFoundResult_WhenGameIdNull(int? recipientUserId)
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Add(null, recipientUserId, "url");

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Add_ReturnsNotFoundResult_WhenGameIsNotFound()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Add(666, identityUser.Id, "url");

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Add_ReturnsNotFoundResult_WhenRecipientUserIdNotFound()
        {
            // Arrange
            GetUserAsyncReturns = identityUser;

            // Act
            var result = await ControllerSUT.Add(game1.GameId, 666, "url");

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [InlineData(null)]
        public async Task Add_ReturnsRedirectResult_WhenSuccessAndUrlNotNullOrEmpty(bool? isGift)
        {
            // Arrange
            User recipientUser = user3;
            GetUserAsyncReturns = identityUser;

            int expectedRecipientUserId = isGift.HasValue && isGift.Value ? recipientUser.Id : identityUser.Id;
            int? recipientUserId = isGift.HasValue ? expectedRecipientUserId : default(int?);

            // Act
            var result = await ControllerSUT.Add(game1.GameId, recipientUserId, "url");

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectResult>(result);
            Assert.Equal("url", redirectResult.Url);
            Assert.IsAssignableFrom<string>(ControllerSUT.TempData["CartAdded"]);

            Assert.Single(await _context.CartGames.ToListAsync());

            CartGame cartItem = await _context.CartGames.FirstOrDefaultAsync();
            Assert.Equal(game1.GameId, cartItem.GameId);
            Assert.Equal(identityUser.Id, cartItem.CartUserId);
            Assert.Equal(expectedRecipientUserId, cartItem.ReceivingUserId);
        }

        [Fact]
        public async Task Add_ReturnsRedirectToActionResult_WhenSuccessAndUrlNullOrEmpty()
        {
            // Arrange
            GetUserAsyncReturns = identityUser;

            // Act
            var result = await ControllerSUT.Add(game1.GameId, identityUser.Id, null);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            Assert.IsAssignableFrom<string>(ControllerSUT.TempData["CartAdded"]);

            Assert.Single(await _context.CartGames.ToListAsync());

            CartGame cartItem = await _context.CartGames.FirstOrDefaultAsync();
            Assert.Equal(game1.GameId, cartItem.GameId);
            Assert.Equal(identityUser.Id, cartItem.CartUserId);
            Assert.Equal(identityUser.Id, cartItem.ReceivingUserId);
        }

        [Fact]
        public async Task Remove_ReturnsNotFoundResult_WhenRecipientUserIdNotFound()
        {
            // Arrange
            GetUserAsyncReturns = identityUser;

            // Act
            var result = await ControllerSUT.Remove(game1.GameId, 666, "url");

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [InlineData(null)]
        public async Task Remove_ReturnsRedirectResult_WhenUrlNotNullOrEmpty(bool? isGift)
        {
            // Arrange
            GetUserAsyncReturns = identityUser;

            User expectedRecipientUser = isGift.HasValue && isGift.Value ? user3 : identityUser;
            int? recipientUserId = isGift.HasValue ? expectedRecipientUser.Id : default(int?);

            _context.CartGames.Add(CreateCartGame(default, identityUser, expectedRecipientUser, game1));
            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Remove(game1.GameId, recipientUserId, "url");

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectResult>(result);
            Assert.Equal("url", redirectResult.Url);
            Assert.Empty(await _context.CartGames.ToListAsync());
            Assert.IsAssignableFrom<string>(ControllerSUT.TempData["CartRemoved"]);
        }

        [Fact]
        public async Task Remove_ReturnsRedirectToActionResult_WhenSuccessAndUrlNullOrEmpty()
        {
            // Arrange
            GetUserAsyncReturns = identityUser;

            // Act
            var result = await ControllerSUT.Remove(game1.GameId, identityUser.Id, null);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            Assert.IsAssignableFrom<string>(ControllerSUT.TempData["CartRemoved"]);
        }

        [Theory]
        [InlineData(666)]
        [InlineData(null)]
        public async Task Gift_ReturnsNotFoundResult_WhenIdIsNullOrNotFound(int? id)
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Gift(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(Relationship.Blocked)]
        [InlineData(Relationship.Pending)]
        public async Task Gift_ReturnsRedirectToActionResult_WhenUserHasNoFriends(Relationship relationship)
        {
            // Arrange
            User blockOrPendingUser = user3;
            GetUserAsyncReturns = identityUser;

            CartGame cartGame = CreateCartGame(default, identityUser, blockOrPendingUser, game1);

            _context.CartGames.Add(cartGame);
            _context.UserRelationships.Add(CreateRelationship(default, identityUser, blockOrPendingUser, relationship));
            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Gift(cartGame.CartGameId);

            // Assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.IsAssignableFrom<string>(ControllerSUT.TempData["FriendsMessage"]);
        }

        [Fact]
        public async Task Gift_ReturnsViewResult_WhenUserHasFriends()
        {
            // Arrange
            User friendUser = user3;
            GetUserAsyncReturns = identityUser;

            CartGame cartGame = CreateCartGame(default, identityUser, identityUser, game1);

            _context.CartGames.Add(cartGame);
            _context.UserRelationships.Add(CreateRelationship(default, identityUser, friendUser, Relationship.Friend));
            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Gift(cartGame.CartGameId);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<SelectList>(ControllerSUT.ViewData["RelatedUserId"]);
            CartGame actual = Assert.IsAssignableFrom<CartGame>(viewResult.Model);
            Assert.Equal(cartGame, actual);
        }

        [Fact]
        public async Task Gift_ReturnsNotFoundResult_WhenIdDoesNotMatch()
        {
            // Arrange
            var postBody = CreateCartGame(666, identityUser, user2, game1);

            // Act
            var result = await ControllerSUT.Gift(1, postBody);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Gift_ReturnsNotFoundResult_WhenUserIsUnexpected()
        {
            // Arrange
            User receivingUser = user2;
            GetUserAsyncReturns = identityUser;

            var postBody = CreateCartGame(1, user3, receivingUser, game1);
            postBody.AddedOn = null;

            // Act
            SimulateModelStateValidation(postBody);
            var result = await ControllerSUT.Gift(1, postBody);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Gift_ReturnsNotFoundResult_WhenReceivingUserIsNotFound()
        {
            // Arrange
            GetUserAsyncReturns = identityUser;

            var postBody = CreateCartGame(default, identityUser.Id, 666, game1.GameId);

            _context.CartGames.Add(postBody);
            await _context.SaveChangesAsync();

            // Act
            SimulateModelStateValidation(postBody);
            var result = await ControllerSUT.Gift(1, postBody);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Gift_ReturnsRedirectToActionResult_WhenDuplicateIndexFound()
        {
            // Arrange
            User receivingUser = user3;
            GetUserAsyncReturns = identityUser;

            CartGame cartGame = CreateCartGame(default, identityUser, receivingUser, game1);

            _context.CartGames.Add(cartGame);
            await _context.SaveChangesAsync();

            var postBody = CreateCartGame(666, identityUser, receivingUser, game1);

            // Act
            SimulateModelStateValidation(postBody);
            var result = await ControllerSUT.Gift(postBody.CartGameId, postBody);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            Assert.IsAssignableFrom<string>(ControllerSUT.TempData["CartError"]);
        }

        [Fact]
        public async Task Gift_ReturnsNotFoundResult_WhenUpdateFailsIdDoesNotExist()
        {
            // Arrange
            User receivingUser = user3;
            GetUserAsyncReturns = identityUser;

            _context.CartGames.Add(CreateCartGame(default, receivingUser, identityUser, game1));
            await _context.SaveChangesAsync();

            var postBody = CreateCartGame(666, identityUser, receivingUser, game1);

            // Act
            SimulateModelStateValidation(postBody);
            var result = await ControllerSUT.Gift(postBody.CartGameId, postBody);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Gift_ReturnsRedirectToActionResult_WhenSuccess()
        {
            // Arrange
            User receivingUser = user3;
            GetUserAsyncReturns = identityUser;

            var postBody = CreateCartGame(default, identityUser, receivingUser, game1);

            _context.CartGames.Add(postBody);
            await _context.SaveChangesAsync();

            // Act
            SimulateModelStateValidation(postBody);
            var result = await ControllerSUT.Gift(postBody.CartGameId, postBody);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task Gift_ReturnsRedirectToActionResult_WhenModelStateInvalidAndNoFriends()
        {
            // Arrange
            User receivingUser = user3;
            GetUserAsyncReturns = identityUser;

            var postBody = CreateCartGame(default, identityUser, receivingUser, game1);

            _context.CartGames.Add(postBody);
            await _context.SaveChangesAsync();

            postBody.AddedOn = null; // <-- date is required

            // Act
            SimulateModelStateValidation(postBody);
            var result = await ControllerSUT.Gift(postBody.CartGameId, postBody);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(nameof(FriendsController.Index), redirectResult.ActionName);
            Assert.Equal("Friends", redirectResult.ControllerName);
            Assert.IsAssignableFrom<string>(ControllerSUT.TempData["FriendsMessage"]);
        }

        [Fact]
        public async Task Gift_ReturnsViewResult_WhenModelStateInvalidAndHasFriends()
        {
            // Arrange
            User receivingUser = user3;
            GetUserAsyncReturns = identityUser;

            var postBody = CreateCartGame(123, identityUser, user2, game1);

            _context.CartGames.Add(postBody);
            _context.UserRelationships.Add(CreateRelationship(default, identityUser, receivingUser, Relationship.Friend));
            await _context.SaveChangesAsync();

            postBody.AddedOn = null; // <-- date is required

            // Act
            SimulateModelStateValidation(postBody);
            var result = await ControllerSUT.Gift(postBody.CartGameId, postBody);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<SelectList>(ControllerSUT.ViewData["RelatedUserId"]);
            Assert.IsAssignableFrom<CartGame>(viewResult.Model);
        }
    }
}
