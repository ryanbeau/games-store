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
    public class AddressControllerTests : DBContextController<AddressController>
    {
        private User identityUser;
        User user1;
        Address address1;

        protected override bool SeedWithApplicationData => false;

        public override AddressController CreateControllerSUT()
        {
            return new AddressController(_context, _mockUserManager.Object);
        }

        public override void SeedDatabase()
        {
            _context.AddRange(
                identityUser = CreateUser(2, IdentityPrincipalUserName, "email1@email.com", true, "Password0!", "Name1", "human", "555-555-5555", new DateTime(1970, 01, 01)),
                user1 = CreateUser(1, IdentityPrincipalUserName, "email2@email.com", true, "Password0!", "Name1", "human", "555-555-5555", new DateTime(1970, 01, 01)),
                address1 = CreateAddress(1, user1.Id, 1, "14 Street", "Toronto", "Ontario", "L0L0L0" )
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
        public async Task Details_ReturnsViewResult_WhenAddressIdIsFound()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Details(1);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<Address>(viewResult.ViewData.Model);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(666)]
        public async Task Details_ReturnsNotFound_WhenAddressIdIsNotFound(int? id)
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Details(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }


        [Fact]
        public async Task Create_ReturnsRedirectToActionResult_WhenAddressIsCreated()
        {
            // Arrange
            GetUserAsyncReturns = identityUser;
            // Act
            var result = await ControllerSUT.Create(new Address { UserId = user1.Id, AddressId = 17, City = "Plop", Street = "17 Plop", Postal ="L0L0L0", Province ="Plopify"});

            // Assert
            var address = Assert.IsAssignableFrom<Address>(_context.Address.FirstOrDefault(w => w.AddressId == 17));
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(1, address.UserId);
            Assert.Equal(17, address.AddressId);
            Assert.Equal("Plop", address.City);
            Assert.Equal("17 Plop", address.Street);
            Assert.Equal("L0L0L0", address.Postal);
            Assert.Equal("Plopify", address.Province);
            Assert.Equal(nameof(AddressController.Index), redirectResult.ActionName);
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

            Assert.IsAssignableFrom<Address>(viewResult.ViewData.Model);

            Assert.NotNull(viewResult);

        }
        [Theory]
        [InlineData(null)]
        [InlineData(666)]
        public async Task Edit_ReturnsNotFound_WhenAddressIdIsNotFound(int? addressId)
        {
            // Arrange
            GetUserAsyncReturns = identityUser;
            // Act
            var result = await ControllerSUT.Edit(addressId);

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
            Assert.IsAssignableFrom<Address>(viewResult.ViewData.Model);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(666)]
        public async Task Delete_ReturnsNotFound_WhenAddressIsNotFound(int? addressId)
        {
            // Arrange
            GetUserAsyncReturns = identityUser;
            // Act
            var result = await ControllerSUT.Delete(addressId);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsRedirectToActionResult_WhenSuccess()
        {
            // Arrange
            GetUserAsyncReturns = user1;

            // Act
            var result = await ControllerSUT.Delete(address1.AddressId);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<Address>(viewResult.ViewData.Model);
        }


        [Fact]
        public async Task DeleteConfirmed_ReturnsRedirectToActionResult_WhenWalletIsDeleted()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.DeleteConfirmed(1);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Null(_context.Address.FirstOrDefault(g => g.AddressId == 1));
            Assert.Equal(nameof(AddressController.Index), redirectResult.ActionName);
        }
    }
}
