using SettlementService.Domain.Abstractions;
using SettlementService.DTO.Booking;

namespace SettlementService.Interfaces.Booking
{
    public interface IBookingService
    {
        Task<Result> AddNewBooking(BookingDto booking);
    }
}
