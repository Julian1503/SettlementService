using SettlementService.Interfaces.Model;

namespace SettlementService.Interfaces
{
    public interface IBookingService
    {
        Task<Guid> AddNewBooking(BookingModel booking);
    }
}
