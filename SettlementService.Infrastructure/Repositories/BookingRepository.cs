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
            throw new NotImplementedException();
        }

        public Task<List<Booking>> GetAllAsync()
        {
            throw new NotImplementedException();
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
