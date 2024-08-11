using AutoMapper;
using Moq;
using SettlementService.Constants.Booking;
using SettlementService.Domain.Abstractions;
using SettlementService.Domain.Entities;
using SettlementService.DTO.Booking;
using SettlementService.Interfaces.Booking;
using SettlementService.Services.Booking;

namespace SettlementService.Services.Test.Services
{
    public class BookingServiceTests
    {
        private IBookingService _service;
        private Mock<IBookingRepository> _mockedBookingRepository;
        private Mock<IMapper> _mockedMapper;
        private Mock<IValidator<BookingDto>> _mockedValidator;

        [SetUp]
        public void SetUp()
        {
            // Initialize mocks and set up the BookingService before each test
            InitializeMocks();
            _service = new BookingService(_mockedBookingRepository.Object, _mockedValidator.Object);
        }

        [Test]
        public async Task AddNewBooking_ShouldCreateABooking()
        {
            // Arrange
            Guid newBookingIdMock = Guid.NewGuid(); // Mocking a new booking ID
            _mockedMapper.Setup(x => x.Map<Domain.Entities.Booking>(It.IsAny<BookingDto>())).Returns(new Domain.Entities.Booking
            {
                Id = newBookingIdMock,
                ClientName = "John Doe",
                BookingTime = new TimeOnly(10, 0)
            });

            // Mocking repository responses
            _mockedBookingRepository.Setup(x => x.CountSimultanousBookings(It.IsAny<TimeOnly>())).ReturnsAsync(2);
            _mockedValidator.Setup(x => x.Validate(It.IsAny<BookingDto>())).ReturnsAsync(Result.Success());
            _mockedBookingRepository.Setup(x => x.CreateAsync(It.IsAny<Domain.Entities.Booking>())).ReturnsAsync(newBookingIdMock);

            // Act
            Result<Guid> newBookingId = await _service.AddNewBooking(new BookingDto
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                BookingTime = "10:00"
            });

            // Assert
            Assert.NotNull(newBookingId);
            Assert.True(newBookingId.isSuccess);
            Assert.That(actual: newBookingId.Value, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public async Task AddNewBooking_ShouldReturnErrorWhenClientNameIsEmpty()
        {
            // Arrange
            _mockedValidator.Setup(x => x.Validate(It.IsAny<BookingDto>())).ReturnsAsync(Result.Failure(BookingConstants.ClientNameRequiredError));

            // Act
            Result response = await _service.AddNewBooking(new BookingDto
            {
                Id = Guid.NewGuid(),
                Name = "",
                BookingTime = "10:00"
            });

            // Assert
            Assert.That(actual: response.isFailure, Is.True); 
            Assert.That(actual: response.Error, Is.EqualTo(BookingConstants.ClientNameRequiredError));
        }

        [Test]
        public async Task AddNewBooking_ShouldReturnFailureWhenBookingTimeIsFull()
        {
            // Arrange
            _mockedValidator.Setup(x => x.Validate(It.IsAny<BookingDto>())).ReturnsAsync(Result.Failure(BookingConstants.BookingFullError));

            // Act
            Result response = await _service.AddNewBooking(new BookingDto
            {
                Id = Guid.NewGuid(),
                Name = "Pepe",
                BookingTime = "10:00"
            });

            // Assert
            Assert.That(actual: response.isFailure, Is.True);
            Assert.That(actual: response.Error, Is.EqualTo(BookingConstants.BookingFullError));
        }

        [Test]
        public async Task AddNewBooking_ShouldReturnFailureWhenBookingTimeIsNotInWorkingHours_LessThanStartTime()
        {
            // Arrange
            _mockedValidator.Setup(x => x.Validate(It.IsAny<BookingDto>())).ReturnsAsync(Result.Failure(BookingConstants.NotWorkingHoursError));

            // Act
            Result response = await _service.AddNewBooking(new BookingDto
            {
                Id = Guid.NewGuid(),
                Name = "Pepe",
                BookingTime = (BookingConstants.START_TIME.Hour - 1).ToString() + ":00" // Time outside of working hours
            });

            // Assert
            Assert.That(actual: response.isFailure, Is.True); 
            Assert.That(actual: response.Error, Is.EqualTo(BookingConstants.NotWorkingHoursError));
        }

        [Test]
        public async Task AddNewBooking_ShouldReturnFailureWhenBookingTimeIsNotInWorkingHours_MoreThanLastBookingTime()
        {
            // Arrange
            _mockedValidator.Setup(x => x.Validate(It.IsAny<BookingDto>())).ReturnsAsync(Result.Failure(BookingConstants.NotWorkingHoursError));

            // Act
            Result response = await _service.AddNewBooking(new BookingDto
            {
                Id = Guid.NewGuid(),
                Name = "Pepe",
                BookingTime = BookingConstants.END_TIME.Hour.ToString() + ":00" // Time outside of working hours
            });

            // Assert
            Assert.That(actual: response.isFailure, Is.True);
            Assert.That(actual: response.Error, Is.EqualTo(BookingConstants.NotWorkingHoursError));
        }

        [Test]
        public async Task AddNewBooking_ShouldReturnFailureWhenTimeFormatIsWrong()
        {
            // Arrange
            _mockedValidator.Setup(x => x.Validate(It.IsAny<BookingDto>())).ReturnsAsync(Result.Failure(BookingConstants.InvalidTimeFormatError));

            // Act
            Result response = await _service.AddNewBooking(new BookingDto
            {
                Id = Guid.NewGuid(),
                Name = "Pepe",
                BookingTime = "25:90" // Invalid time format
            });

            // Assert
            Assert.That(actual: response.isFailure, Is.True); 
            Assert.That(actual: response.Error, Is.EqualTo(BookingConstants.InvalidTimeFormatError));
        }

        #region MOCK Init
        private void InitializeMocks()
        {
            // Initialize and configure all mocks
            _mockedBookingRepository = new Mock<IBookingRepository>();
            _mockedMapper = new Mock<IMapper>();
            _mockedValidator = new Mock<IValidator<BookingDto>>();
            SetupMockedMapper();
            SetupMockedBookingRepository();
        }
        private void SetupMockedMapper()
        {
            // Set up mock behavior for IMapper
            _mockedMapper.Setup(x => x.Map<Domain.Entities.Booking>(It.IsAny<BookingDto>())).Returns(new Domain.Entities.Booking
            {
                Id = Guid.NewGuid(),
                ClientName = "John Doe",
                BookingTime = new TimeOnly(10, 0)
            });
        }

        private void SetupMockedBookingRepository()
        {
            // Set up mock behavior for IBookingRepository
            _mockedBookingRepository.Setup(x => x.CountSimultanousBookings(It.IsAny<TimeOnly>())).ReturnsAsync(2);
            _mockedBookingRepository.Setup(x => x.CreateAsync(It.IsAny<Domain.Entities.Booking>())).ReturnsAsync(Guid.NewGuid());
        }

        #endregion
    }
}