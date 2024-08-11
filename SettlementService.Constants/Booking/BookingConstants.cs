using System.Net;
using SettlementService.Domain.Abstractions;

namespace SettlementService.Constants.Booking
{
    public static class BookingConstants
    {
        public static readonly TimeOnly START_TIME = new TimeOnly(9, 0);
        public static readonly TimeOnly END_TIME = new TimeOnly(17, 0);
        public static readonly TimeOnly LAST_BOOKING_TIME = END_TIME.AddMinutes(-DURATION);
        public const int DURATION = 60;
        public const int MAX_SIMULTANEOUS_BOOKINGS = 4;

        public static readonly Error BookingFullError = new Error("Booking at this time is full.", HttpStatusCode.Conflict);
        public static readonly Error NotWorkingHoursError = new Error("Booking time is not in the working hours.", HttpStatusCode.BadRequest);
        public static readonly Error ClientNameRequiredError = new Error("Client Name is required.", HttpStatusCode.BadRequest);
        public static readonly Error InvalidTimeFormatError = new Error("Invalid time format.", HttpStatusCode.BadRequest);
    }
}
