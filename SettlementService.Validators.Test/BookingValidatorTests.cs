using Moq;
using SettlementService.Constants.Booking;
using SettlementService.Domain.Abstractions;
using SettlementService.DTO.Booking;

namespace SettlementService.Validators.Test
{
    [TestFixture]
    public class BookingValidatorTests
    {
        private Mock<IBookingRepository> _mockBookingRepository;
        private IValidator<BookingDto> _validator;

        [SetUp]
        public void Setup()
        {
            _mockBookingRepository = new Mock<IBookingRepository>();
            _validator = new BookingValidator(_mockBookingRepository.Object);
        }

        [Test]
        public async Task Validate_ShouldReturnFailure_WhenNameIsNullOrEmpty()
        {
            // Arrange
            var bookingDto = new BookingDto
            {
                Name = string.Empty,
                BookingTime = "10:00"
            };

            // Act
            Result result = await _validator.Validate(bookingDto);

            // Assert
            Assert.IsFalse(result.isSuccess);
            Assert.That(actual: result.Error, Is.EqualTo(BookingConstants.ClientNameRequiredError));
        }

        [Test]
        public async Task Validate_ShouldReturnFailure_WhenBookingTimeIsInvalid()
        {
            // Arrange
            var bookingDto = new BookingDto
            {
                Name = "John Doe",
                BookingTime = "invalid-time"
            };

            // Act
            var result = await _validator.Validate(bookingDto);

            // Assert
            Assert.IsFalse(result.isSuccess);
            Assert.That(actual: result.Error, Is.EqualTo(BookingConstants.InvalidTimeFormatError));
        }

        [Test]
        public async Task Validate_ShouldReturnFailure_WhenBookingTimeIsOutsideWorkingHours()
        {
            // Arrange
            var bookingDto = new BookingDto
            {
                Name = "John Doe",
                BookingTime = "08:00" // Assuming this is outside working hours
            };

            // Act
            var result = await _validator.Validate(bookingDto);

            // Assert
            Assert.IsFalse(result.isSuccess);
            Assert.That(actual: result.Error, Is.EqualTo(BookingConstants.NotWorkingHoursError));
        }

        [Test]
        public async Task Validate_ShouldReturnFailure_WhenBookingIsFull()
        {
            // Arrange
            var bookingDto = new BookingDto
            {
                Name = "John Doe",
                BookingTime = "10:00"
            };

            _mockBookingRepository.Setup(x => x.CountSimultanousBookings(It.IsAny<TimeOnly>()))
                .ReturnsAsync(BookingConstants.MAX_SIMULTANEOUS_BOOKINGS);

            // Act
            var result = await _validator.Validate(bookingDto);

            // Assert
            Assert.IsFalse(result.isSuccess);
            Assert.That(actual: result.Error, Is.EqualTo(BookingConstants.BookingFullError));
        }

        [Test]
        public async Task Validate_ShouldReturnSuccess_WhenBookingIsValid()
        {
            // Arrange
            var bookingDto = new BookingDto
            {
                Name = "John Doe",
                BookingTime = "10:00"
            };

            _mockBookingRepository.Setup(x => x.CountSimultanousBookings(It.IsAny<TimeOnly>()))
                .ReturnsAsync(0); // No bookings at the given time

            // Act
            Result result = await _validator.Validate(bookingDto);

            // Assert
            Assert.IsTrue(result.isSuccess);
            Assert.That(result, 
                Is.EqualTo(Result.Success())
                .Using<Result>((e, a) => 
                e.isSuccess == a.isSuccess && 
                e.Error == a.Error && 
                a.isFailure == e.isFailure));
        }
    }
}