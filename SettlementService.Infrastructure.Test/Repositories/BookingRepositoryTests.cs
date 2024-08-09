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
        private readonly DataContext _context;
        private readonly BookingRepository _repository;

        [OneTimeTearDown]
        public void DisposeContext() 
        {
            _context.Dispose();
        }

        public BookingRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase(databaseName: "TestDB")
           .Options;

            _context = new DataContext(options);
            _repository = new BookingRepository(_context);
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
    }
}
