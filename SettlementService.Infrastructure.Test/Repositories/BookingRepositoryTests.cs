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
            Booking booking = new Booking { Id = Guid.NewGuid(), ClientName = "Julian Delgado", BookingTime = new TimeOnly(12,0) };
            Guid newBookingId = await _repository.CreateAsync(booking);
            Booking? newBooking = await _context.Bookings.FindAsync(newBookingId);

            Assert.That(actual: newBookingId, Is.Not.EqualTo(Guid.Empty));
            Assert.NotNull(newBooking);
        }

        [Test]
        public async Task Create_ShouldAddANewEntityToTheDatabaseWithCorrectValues()
        {
            Booking booking = new Booking { Id = Guid.NewGuid(), ClientName = "Julian Delgado", BookingTime = new TimeOnly(12,0) };
            Guid newBookingId = await _repository.CreateAsync(booking);
            Booking? newBooking = await _context.Bookings.FindAsync(newBookingId);

            Assert.That(actual: newBooking?.Id, Is.EqualTo(booking.Id));
            Assert.That(actual: newBooking?.ClientName, Is.EqualTo(booking.ClientName));
            Assert.That(actual: newBooking?.BookingTime, Is.EqualTo(booking.BookingTime));
        }

        [Test]
        public async Task GetAll_ShouldBringAllBookings()
        {
            //override bookings from context to test
            List<Booking> bookings = (await _repository.GetAllAsync()).ToList();
            Assert.NotNull(bookings);
            Assert.IsTrue(bookings.Count > 0);
        }

        [Test]
        public async Task GetAll_ShouldBringAllBookingsWithCorrectValues()
        {
            //override bookings from context to test
            List<Booking> bookings = (await _repository.GetAllAsync()).ToList();
            Assert.NotNull(bookings);
            Assert.IsTrue(bookings.Count > 0);
            Assert.That(actual: bookings[0].ClientName, Is.EqualTo("Juan"));
            Assert.That(actual: bookings[1].ClientName, Is.EqualTo("Pepe"));
        }

        [Test]
        public async Task GetById_ShouldBringABooking()
        {
            Booking booking = await _repository.GetByIdAsync(_context.Bookings.First().Id);
            Assert.NotNull(booking);
        }

        [Test]
        public void GetById_ShouldThrowKeyNotFoundException()
        {
            Guid nonExistentId = Guid.NewGuid();
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.GetByIdAsync(nonExistentId));
            Assert.That(actual: ex.Message, Is.EqualTo("Booking not found."));
        }

        [Test]
        public async Task GetById_ShouldBringABookingWithCorrectValues()
        {
            Booking booking = await _repository.GetByIdAsync(_context.Bookings.First().Id);
            Assert.NotNull(booking);
            Assert.That(actual: booking.ClientName, Is.EqualTo("Juan"));
        }
    }
}
