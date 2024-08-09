using AutoMapper;
using Moq;
using SettlementService.Constants;
using SettlementService.Domain.Abstractions;
using SettlementService.Domain.Entities;
using SettlementService.Interfaces;
using SettlementService.Interfaces.Model;

namespace SettlementService.Services.Test.Services
{
    public class BookingServiceTests
    {
        private IBookingService _service;
        private Mock<IBookingRepository> _mockedBookingRepository;
        private Mock<IMapper> _mockedMapper;

        [SetUp]
        public void SetUp()
        {
            InitializeMocks();
            _service = new BookingService(_mockedBookingRepository.Object, _mockedMapper.Object);
        }

        [Test]
        public async Task AddNewBooking_ShouldCreateABooking()
        {
            Guid newBookingIdMock = Guid.NewGuid();
            _mockedMapper.Setup(x => x.Map<Booking>(It.IsAny<BookingModel>())).Returns(new Booking
            {
                Id = newBookingIdMock,
                ClientName = "John Doe",
                BookingTime = new TimeOnly(10, 0)
            });

            _mockedBookingRepository.Setup(x => x.GetByTimeAsync(It.IsAny<TimeOnly>())).ReturnsAsync(new List<Booking>()
            {
                new Booking { Id = Guid.NewGuid(), ClientName = "Juan", BookingTime = new TimeOnly(10,0) },
                new Booking { Id = Guid.NewGuid(), ClientName = "Pepe", BookingTime = new TimeOnly(11,0) }
            });

            _mockedBookingRepository.Setup(x => x.CreateAsync(It.IsAny<Booking>())).ReturnsAsync(newBookingIdMock);

            Guid newBookingId = await _service.AddNewBooking(new BookingModel
            {
                ClientName = "John Doe",
                BookingTime = new TimeOnly(10, 0)
            });

            
            Assert.NotNull(newBookingId);
            Assert.That(actual: newBookingId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void AddNewBooking_ShouldThrowArgumentExceptionWhenClientNameIsEmpty()
        {
            Assert.ThrowsAsync<ArgumentException>(() => _service.AddNewBooking(new BookingModel
            {
                ClientName = "",
                BookingTime = new TimeOnly(10, 0)
            }));
        }

        [Test]
        public void AddNewBooking_ShouldThrowInvalidOperationExceptionWhenBookingTimeIsFull()
        {
            _mockedBookingRepository.Setup(x => x.GetByTimeAsync(It.IsAny<TimeOnly>())).ReturnsAsync(new List<Booking>()
            {
                new Booking { Id = Guid.NewGuid(), ClientName = "Juan", BookingTime = new TimeOnly(10,0) },
                new Booking { Id = Guid.NewGuid(), ClientName = "Pepe", BookingTime = new TimeOnly(11,0) },
                new Booking { Id = Guid.NewGuid(), ClientName = "Perez", BookingTime = new TimeOnly(12,0) },
                new Booking { Id = Guid.NewGuid(), ClientName = "Tito", BookingTime = new TimeOnly(13,0) }
            });

            Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddNewBooking(new BookingModel
            {
                ClientName = "John Doe",
                BookingTime = new TimeOnly(10, 0)
            }));
        }

        [Test]
        public void AddNewBooking_ShouldThrowArgumentExceptionWhenBookingTimeIsNotInWorkingHours_LessThanStartTime()
        {
            Assert.ThrowsAsync<ArgumentException>(() => _service.AddNewBooking(new BookingModel
            {
                ClientName = "John Doe",
                BookingTime = new TimeOnly(BookingConstants.START_TIME.Hour - 1, 0)
            }));
        }

        [Test]
        public void AddNewBooking_ShouldThrowArgumentExceptionWhenBookingTimeIsNotInWorkingHours_MoreThanLastBookingTime()
        {
            Assert.ThrowsAsync<ArgumentException>(() => _service.AddNewBooking(new BookingModel
            {
                ClientName = "John Doe",
                BookingTime = new TimeOnly(BookingConstants.END_TIME.Hour, 0)
            }));
        }

        #region MOCK Init
        private void InitializeMocks()
        {
            _mockedBookingRepository = new Mock<IBookingRepository>();
            _mockedMapper = new Mock<IMapper>();
            SetupMockedMapper();
            SetupMockedBookingRepository();
        }
        private void SetupMockedMapper()
        {
            _mockedMapper.Setup(x => x.Map<Booking>(It.IsAny<BookingModel>())).Returns(new Booking
            {
                Id = Guid.NewGuid(),
                ClientName = "John Doe",
                BookingTime = new TimeOnly(10, 0)
            });
        }

        private void SetupMockedBookingRepository()
        {
            _mockedBookingRepository.Setup(x => x.GetByTimeAsync(It.IsAny<TimeOnly>())).ReturnsAsync(new List<Booking>()
            {
                new Booking { Id = Guid.NewGuid(), ClientName = "Juan", BookingTime = new TimeOnly(10,0) },
                new Booking { Id = Guid.NewGuid(), ClientName = "Pepe", BookingTime = new TimeOnly(11,0) }
            });

            _mockedBookingRepository.Setup(x => x.CreateAsync(It.IsAny<Booking>())).ReturnsAsync(Guid.NewGuid());
        }

    }
#endregion
}