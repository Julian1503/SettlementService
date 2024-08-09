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

        public Task<Booking> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Booking>> GetByTimeAsync(TimeOnly bookingTime)
        {
            throw new NotImplementedException();
        }
    }
}
