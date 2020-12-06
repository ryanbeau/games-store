using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sprint.Controllers;
using Sprint.Models;
using Sprint.Tests.Helpers;
using Sprint.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Sprint.Tests.Controllers
{
    public class EventControllerTests : DBContextController<EventController>
    {

        public override EventController CreateControllerSUT()
        {
            return new EventController(_mockUserManager.Object, _context);
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
        public async Task Details_ReturnsViewResult_WhenEventIdIsFound()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Details(1);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<EventViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Details(null);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }


        [Fact]
        public void Create_ReturnsViewResult()
        {
            // Arrange
            GetUserAsyncReturns = new User { Id = 1 };

            // Act
            var result = ControllerSUT.Create();

            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsRedirectResult_WhenEventIsAdded()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Create(new Event { EventId = 1, UserId = 1 });

            // Assert

            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);

            Assert.Equal(nameof(EventController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task Create_ReturnsView_WhenModelStateIsInvalid()
        {
            // Arrange
            var activity = new Event { EventId = 1, UserId = 1 };
            SimulateModelStateValidation(activity);

            // Act
            var result = await ControllerSUT.Create(activity);

            // Assert

            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);

            Assert.IsAssignableFrom<Event>(viewResult.Model);
        }

        [Fact]
        public async Task Add_ReturnsRedirectToActionResult_WhenEventUserIsAdded()
        {
            // Arrange
            User user = _context.Users.FirstOrDefault();
            GetUserAsyncReturns = user;
            Event activity = new Event
            {
                UserId = user.Id,
                EventDateTime = DateTime.Now,
                EventDescription = "whatever",
                EventName = "whatever2",
            };
            _context.Add(activity);

            _context.EventUsers.Add(new EventUser
            {
                UserId = activity.UserId,
                EventId = activity.EventId,
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Add(activity.UserId, "url");

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);

            //Assert.Equal(nameof(EventController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenEventIdIsNull()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Edit(null);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenEventIdNotFound()
        {
            // Arrange


            // Act
            var result = await ControllerSUT.Edit(666);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsView_WhenEventIdIsFound()
        {
            // Arrange
            User user = _context.Users.FirstOrDefault();
            Event activity = new Event
            {
                UserId = user.Id,
                EventDateTime = DateTime.Now,
                EventDescription = "whatever",
                EventName = "whatever2",
            };
            _context.Add(activity);
            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Edit(activity.EventId);

            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenEventIdIsFoundOnSave()
        {
            // Arrange
            User user = _context.Users.FirstOrDefault();
            Event activity = new Event
            {
                UserId = user.Id,
                EventId = 2,
                EventDateTime = DateTime.Now,
                EventDescription = "whatever",
                EventName = "whatever2",
            };
            _context.Add(activity);
            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Edit(666, activity);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WhenSuccessful()
        {
            // Arrange
            User user = _context.Users.FirstOrDefault();
            Event activity = new Event
            {
                UserId = user.Id,
                EventId = 2,
                EventDateTime = DateTime.Now,
                EventDescription = "whatever",
                EventName = "whatever2",
            };
            _context.Add(activity);
            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Edit(activity.EventId, activity);

            // Assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            User user = _context.Users.FirstOrDefault();
            Event activity = new Event
            {
                EventId = 2,
                EventDateTime = DateTime.Now,
            };
            _context.Add(activity);
            await _context.SaveChangesAsync();

            SimulateModelStateValidation(activity);

            // Act
            var result = await ControllerSUT.Edit(activity.EventId, activity);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            Assert.IsAssignableFrom<Event>(viewResult.Model);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenNotFound()
        {
            // Arrange
            User user = _context.Users.FirstOrDefault();
            Event activity = new Event
            {
                UserId = user.Id,
                EventId = 666,
                EventDateTime = DateTime.Now,
                EventDescription = "whatever",
                EventName = "whatever2",
            };

            _context.Add(activity);

            Event activity2 = new Event
            {
                UserId = user.Id,
                EventId = 2,
                EventDateTime = DateTime.Now,
                EventDescription = "whatever",
                EventName = "whatever2",
            };

            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Edit(activity2.EventId, activity2);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsView_WhenEventIsFound() 
        { 
            // Arrange
            User user = _context.Users.FirstOrDefault();
            Event activity = new Event
            {
                UserId = user.Id,
                EventDateTime = DateTime.Now,
                EventDescription = "whatever",
                EventName = "whatever2",
            };
            _context.Add(activity);
            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Delete(activity.EventId);

            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenEventIsNotFound()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Delete(1);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenEventIsNull()
        {
            // Arrange

            // Act
            var result = await ControllerSUT.Delete(null);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_ReturnsRedirectToActionResult_WhenDeleteEventIsConfirmed()
        {
            // Arrange
            User user = _context.Users.FirstOrDefault();
            Event activity = new Event
            {
                UserId = user.Id,
                EventDateTime = DateTime.Now,
                EventDescription = "whatever",
                EventName = "whatever2",
            };
            _context.Add(activity);

            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.DeleteConfirmed(activity.EventId);

            // Assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Remove_ReturnsRedirectToAction_WhenEventIsFound()
        {
            // Arrange
            User user = _context.Users.FirstOrDefault();
            Event activity = new Event
            {
                UserId = user.Id,
                EventDateTime = DateTime.Now,
                EventDescription = "whatever",
                EventName = "whatever2",
            };
            GetUserAsyncReturns = _context.Users.FirstOrDefault();
            _context.Add(new EventUser
            {
                User = user,
                Event = activity
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Remove(1, "url");

            // Assert
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Remove_ReturnsNotFound_WhenEventIsNotFound()
        {
            // Arrange
            User user = _context.Users.FirstOrDefault();
            Event activity = new Event
            {
                UserId = user.Id,
                EventDateTime = DateTime.Now,
                EventDescription = "whatever",
                EventName = "whatever2",
            };
            GetUserAsyncReturns = _context.Users.FirstOrDefault();
            _context.Add(new EventUser
            {
                User = user,
                Event = activity
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await ControllerSUT.Remove(666, "url");

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

    }
}
