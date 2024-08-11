using Microsoft.EntityFrameworkCore;
using SettlementService.Domain.Abstractions;
using SettlementService.Domain.Entities;

namespace SettlementService.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext _context;

        public BookingRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(Booking booking)
        {   
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking.Id;
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking> GetByIdAsync(Guid bookingId)
        {
            Booking? booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == bookingId);

            if (booking == null)
            {
                throw new KeyNotFoundException($"Booking not found.");
            }

            return booking;
        }

        public async Task<int> CountSimultanousBookings(TimeOnly bookingTime)
        {
            int simultanousBookings = await _context.Bookings.CountAsync(bookings => bookings.BookingTime >= bookingTime.AddMinutes(-59) && bookings.BookingTime <= bookingTime.AddMinutes(59));

            return simultanousBookings;
        }
    }
}
