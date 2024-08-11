namespace SettlementService.API.Response.Booking
{
    public class BookingAddResponse
    {
        public Guid BookingId { get; }
        public BookingAddResponse(Guid bookingId) {
            BookingId = bookingId;
        }
    }
}
