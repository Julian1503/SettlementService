using System.Net;
using SettlementService.Domain.Abstractions;

namespace SettlementService.Constants.Booking
{
    public static class BookingConstants
    {
        /// <summary>
        /// Value for the start time of the working hours
        /// </summary>
        public static readonly TimeOnly START_TIME = new TimeOnly(9, 0);

        /// <summary>
        /// Value for the end time of the working hours
        /// </summary>
        public static readonly TimeOnly END_TIME = new TimeOnly(17, 0);

        /// <summary>
        /// Value for the last booking time accepted
        /// </summary>
        public static readonly TimeOnly LAST_BOOKING_TIME = END_TIME.AddMinutes(-DURATION);

        /// <summary>
        /// Value for the maximum length of the client name
        /// </summary>
        public static readonly int MAX_NAME_LENGTH = 250;

        /// <summary>
        /// Duration of each booking in minutes
        /// </summary>
        public const int DURATION = 60;

        /// <summary>
        /// Maximum number of bookings that can be made at the same time
        /// </summary>
        public const int MAX_SIMULTANEOUS_BOOKINGS = 4;

        /// <summary>
        /// Error when the booking is full
        /// </summary>
        public static readonly Error BookingFullError = new Error("Conflict", "Booking at this time is full.", 409);

        /// <summary>
        /// Error when the booking time is not in the working hours
        /// </summary>
        public static readonly Error NotWorkingHoursError = new Error("Bad Request", "Booking time is not in the working hours.", 400);

        /// <summary>
        /// Error when the client name is empty or null
        /// </summary>
        public static readonly Error ClientNameRequiredError = new Error("Bad Request", "Client Name is required.", 400);

        /// <summary>
        /// Error when the booking time is with a wrong format
        /// </summary>
        public static readonly Error InvalidTimeFormatError = new Error("Bad Request", "Invalid time format.", 400);

        /// <summary>
        /// Error when the client name is too long
        /// </summary>
        public static readonly Error ClientNameTooLongError = new Error("Bad Request", "Client Name too long.", 400);
    }
}
