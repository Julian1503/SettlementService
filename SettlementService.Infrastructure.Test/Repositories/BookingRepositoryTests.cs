using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SettlementService.Domain.Abstractions;
using SettlementService.Domain.Entities;
using SettlementService.Infrastructure.Repositories;

namespace SettlementService.Infrastructure.Test.Repositories
{
    public class BookingRepositoryTests
    {
        private DataContext _context;
        private BookingRepository _repository;


        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase(databaseName: "TestDB")
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
            Booking newBooking = await _context.Bookings.FindAsync(newBookingId);

            Assert.AreNotEqual(expected: Guid.Empty, actual: newBookingId);
            Assert.NotNull(newBooking);
        }

        [Test]
        public async Task Create_ShouldAddANewEntityToTheDatabaseWithCorrectValues()
        {
            Booking booking = new Booking { Id = Guid.NewGuid(), ClientName = "Julian Delgado", BookingTime = new TimeOnly(12,0) };
            Guid newBookingId = await _repository.CreateAsync(booking);
            Booking newBooking = await _context.Bookings.FindAsync(newBookingId);

            Assert.AreEqual(expected: booking.Id, actual: newBooking.Id);
            Assert.AreEqual(expected: booking.ClientName, actual: newBooking.ClientName);
            Assert.AreEqual(expected: booking.BookingTime, actual: newBooking.BookingTime);
        }

        [Test]
        public async Task GetAll_ShouldBringAllBookings()
        {
            //override bookings from context to test
            List<Booking> bookings = await _repository.GetAllAsync();
            Assert.NotNull(bookings);
            Assert.IsTrue(bookings.Count > 0);
        }

        [Test]
        public async Task GetAll_ShouldBringAllBookingsWithCorrectValues()
        {
            //override bookings from context to test
            List<Booking> bookings = await _repository.GetAllAsync();
            Assert.NotNull(bookings);
            Assert.IsTrue(bookings.Count > 0);
            Assert.AreEqual(expected: "Juan", actual: bookings[0].ClientName);
            Assert.AreEqual(expected: "Pepe", actual: bookings[1].ClientName);
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
            Assert.AreEqual("Booking not found.", ex.Message);
        }

        [Test]
        public async Task GetById_ShouldBringABookingWithCorrectValues()
        {
            Booking booking = await _repository.GetByIdAsync(_context.Bookings.First().Id);
            Assert.NotNull(booking);
            Assert.AreEqual(expected: "Juan", actual: booking.ClientName);
        }
    }
}
