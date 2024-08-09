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
            var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == bookingId);

            return booking;
        }

        public Task<List<Booking>> GetByTimeAsync(TimeOnly bookingTime)
        {
            throw new NotImplementedException();
        }
    }
}
