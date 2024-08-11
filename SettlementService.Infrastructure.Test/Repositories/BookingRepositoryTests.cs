using Microsoft.EntityFrameworkCore;
using SettlementService.Domain.Abstractions;
using SettlementService.Domain.Entities;
using SettlementService.Infrastructure.Repositories;

namespace SettlementService.Infrastructure.Test.Repositories
{
    [TestFixture]
    public class BookingRepositoryTests
    {
        private DataContext _context;
        private IBookingRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new DataContext(options);
            _repository = new BookingRepository(_context);

            SeedDatabase();
        }

        [TearDownAttribute]
        public void DisposeContext()
        {
            _context.Dispose();
        }
        private void SeedDatabase()
        {
            var bookings = new List<Booking>
            {
                new Booking { Id = Guid.NewGuid(), ClientName = "Juan", BookingTime = new TimeOnly(10,0) },
                new Booking { Id = Guid.NewGuid(), ClientName = "Pepe", BookingTime = new TimeOnly(11,0) }
            };

            _context.Bookings.AddRange(bookings);
            _context.SaveChanges();
        }

        [Test]
        public async Task Create_ShouldAddANewEntityToTheDatabase()
        {
            //Arrange
            Booking booking = new Booking { Id = Guid.NewGuid(), ClientName = "Julian Delgado", BookingTime = new TimeOnly(12,0) };
            Guid newBookingId = await _repository.CreateAsync(booking);

            //Act
            Booking? newBooking = await _context.Bookings.FindAsync(newBookingId);

            //Assert
            Assert.That(actual: newBookingId, Is.Not.EqualTo(Guid.Empty));
            Assert.NotNull(newBooking);
        }

        [Test]
        public async Task Create_ShouldAddANewEntityToTheDatabaseWithCorrectValues()
        {
            //Arrange
            Booking booking = new Booking { Id = Guid.NewGuid(), ClientName = "Julian Delgado", BookingTime = new TimeOnly(12,0) };
            Guid newBookingId = await _repository.CreateAsync(booking);

            //Act
            Booking? newBooking = await _context.Bookings.FindAsync(newBookingId);

            //Assert
            Assert.That(actual: newBooking?.Id, Is.EqualTo(booking.Id));
            Assert.That(actual: newBooking?.ClientName, Is.EqualTo(booking.ClientName));
            Assert.That(actual: newBooking?.BookingTime, Is.EqualTo(booking.BookingTime));
        }

        [Test]
        public async Task GetAll_ShouldBringAllBookings()
        {
            //Act
            List<Booking> bookings = (await _repository.GetAllAsync()).ToList();

            //Assert
            Assert.NotNull(bookings);
            Assert.IsTrue(bookings.Count > 0);
        }

        [Test]
        public async Task GetAll_ShouldBringAllBookingsWithCorrectValues()
        {
            //Act
            List<Booking> bookings = (await _repository.GetAllAsync()).ToList();

            //Assert
            Assert.NotNull(bookings);
            Assert.IsTrue(bookings.Count > 0);
            Assert.That(actual: bookings[0].ClientName, Is.EqualTo("Juan"));
            Assert.That(actual: bookings[1].ClientName, Is.EqualTo("Pepe"));
        }

        [Test]
        public async Task GetById_ShouldBringABooking()
        {
            //Act
            Booking booking = await _repository.GetByIdAsync(_context.Bookings.First().Id);

            //Assert
            Assert.NotNull(booking);
        }

        [Test]
        public void GetById_ShouldThrowKeyNotFoundException()
        {
            //Arrange
            Guid nonExistentId = Guid.NewGuid();

            //Act
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.GetByIdAsync(nonExistentId));

            //Assert
            Assert.That(actual: ex.Message, Is.EqualTo("Booking not found."));
        }

        [Test]
        public async Task GetById_ShouldBringABookingWithCorrectValues()
        {
            //Arrange
            Booking booking = await _repository.GetByIdAsync(_context.Bookings.First().Id);

            //Act
            Assert.NotNull(booking);

            //Assert
            Assert.That(actual: booking.ClientName, Is.EqualTo("Juan"));
        }
    }
}
