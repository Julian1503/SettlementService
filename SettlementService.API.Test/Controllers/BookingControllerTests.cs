using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SettlementService.API.Controllers;
using SettlementService.API.Response.Booking;
using SettlementService.Constants.Booking;
using SettlementService.Domain.Abstractions;
using SettlementService.DTO.Booking;
using SettlementService.Interfaces.Booking;

namespace SettlementService.API.Test.Controllers
{
    [TestFixture]
    public class BookingControllerTests
    {
        private Mock<IBookingService> _mockedBookingService;
        private BookingController _controller;

        [SetUp]
        public void Setup()
        {
            _mockedBookingService = new Mock<IBookingService>();
            _controller = new BookingController(_mockedBookingService.Object);
        }

        [Test]
        public async Task AddNewBooking_ShouldReturnOk_WhenBookingIsCreated()
        {
            // Arrange
            var bookingDto = new BookingDto { Id = Guid.NewGuid(), Name = "John Doe", BookingTime = "10:00" };
            var bookingId = Guid.NewGuid();
            _mockedBookingService.Setup(x => x.AddNewBooking(It.IsAny<BookingDto>())).ReturnsAsync(Result.Success(bookingId));

            // Act
            var result = await _controller.AddNewBooking(bookingDto) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.That(actual: result.StatusCode, Is.EqualTo(200));
            Assert.That(actual: (result.Value as BookingAddResponse).BookingId, Is.EqualTo(bookingId));
        }

        [Test]
        public async Task AddNewBooking_ShouldReturnBadRequest_WhenNameIsEmpty()
        {
            // Arrange
            var bookingDto = new BookingDto { Id = Guid.NewGuid(), Name = "", BookingTime = "10:00" };
            _mockedBookingService.Setup(x => x.AddNewBooking(It.IsAny<BookingDto>())).ReturnsAsync(Result.Failure<Guid>(BookingConstants.ClientNameRequiredError));

            // Act
            var result = await _controller.AddNewBooking(bookingDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.That(actual: ((ProblemDetails)result.Value).Title, Is.EqualTo(BookingConstants.ClientNameRequiredError.Title));
            Assert.That(actual: ((ProblemDetails)result.Value).Status, Is.EqualTo(BookingConstants.ClientNameRequiredError.Status));
            Assert.That(actual: ((ProblemDetails)result.Value).Detail, Is.EqualTo(BookingConstants.ClientNameRequiredError.Detail));
        }

        [Test]
        public async Task AddNewBooking_ShouldReturnBadRequest_WhenTimeFormatIsWrong()
        {
            // Arrange
            var bookingDto = new BookingDto { Id = Guid.NewGuid(), Name = "John Doe", BookingTime = "25:90" };
            _mockedBookingService.Setup(x => x.AddNewBooking(It.IsAny<BookingDto>())).ReturnsAsync(Result.Failure<Guid>(BookingConstants.InvalidTimeFormatError));

            //Act
            var result = await _controller.AddNewBooking(bookingDto) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.That(actual: ((ProblemDetails)result.Value).Title, Is.EqualTo(BookingConstants.InvalidTimeFormatError.Title));
            Assert.That(actual: ((ProblemDetails)result.Value).Status, Is.EqualTo(BookingConstants.InvalidTimeFormatError.Status));
            Assert.That(actual: ((ProblemDetails)result.Value).Detail, Is.EqualTo(BookingConstants.InvalidTimeFormatError.Detail));
        }

        [Test]
        public async Task AddNewBooking_ShouldReturnBadRequest_WhenBookingTimeIsNotInWorkingHours_LessThanMin()
        {
            // Arrange
            var bookingDto = new BookingDto { Id = Guid.NewGuid(), Name = "John Doe", BookingTime = "05:00" };
            _mockedBookingService.Setup(x => x.AddNewBooking(It.IsAny<BookingDto>())).ReturnsAsync(Result.Failure<Guid>(BookingConstants.NotWorkingHoursError));
            
            //Act
            var result = await _controller.AddNewBooking(bookingDto) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.That(actual: ((ProblemDetails)result.Value).Title, Is.EqualTo(BookingConstants.NotWorkingHoursError.Title));
            Assert.That(actual: ((ProblemDetails)result.Value).Status, Is.EqualTo(BookingConstants.NotWorkingHoursError.Status));
            Assert.That(actual: ((ProblemDetails)result.Value).Detail, Is.EqualTo(BookingConstants.NotWorkingHoursError.Detail));
        }

        [Test]
        public async Task AddNewBooking_ShouldReturnBadRequest_WhenBookingTimeIsNotInWorkingHours_MoreThanMax()
        {
            // Arrange
            var bookingDto = new BookingDto { Id = Guid.NewGuid(), Name = "John Doe", BookingTime = "19:00" };
            _mockedBookingService.Setup(x => x.AddNewBooking(It.IsAny<BookingDto>())).ReturnsAsync(Result.Failure<Guid>(BookingConstants.NotWorkingHoursError));

            //Act
            var result = await _controller.AddNewBooking(bookingDto) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.That(actual: ((ProblemDetails)result.Value).Title, Is.EqualTo(BookingConstants.NotWorkingHoursError.Title));
            Assert.That(actual: ((ProblemDetails)result.Value).Status, Is.EqualTo(BookingConstants.NotWorkingHoursError.Status));
            Assert.That(actual: ((ProblemDetails)result.Value).Detail, Is.EqualTo(BookingConstants.NotWorkingHoursError.Detail));
        }

    }
}
