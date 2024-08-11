using SettlementService.Domain.Abstractions;
using SettlementService.DTO.Booking;

namespace SettlementService.Interfaces.Booking
{
    /// <summary>
    /// Represents the booking service
    /// </summary>
    public interface IBookingService
    {
        Task<Result<Guid>> AddNewBooking(BookingDto booking);
    }
}
