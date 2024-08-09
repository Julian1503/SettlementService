using SettlementService.Domain.Entities;

namespace SettlementService.Domain.Abstractions
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<List<Booking>> GetByTime(TimeOnly bookingTime);
    }
}
