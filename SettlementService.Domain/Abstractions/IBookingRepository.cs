using SettlementService.Domain.Entities;

namespace SettlementService.Domain.Abstractions
{
    public interface IBookingRepository : IRepository<Booking>
    {
        /// <summary>
        /// Returns the number of simultaneous bookings at a given time
        /// </summary>
        /// <param name="bookingTime">Time of the booking that needs to be looked</param>
        /// <returns>Number of simultaneous bookings (int)</returns>
        Task<int> CountSimultanousBookings(TimeOnly bookingTime);
    }
}
