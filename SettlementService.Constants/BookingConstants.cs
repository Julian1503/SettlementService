namespace SettlementService.Constants
{
    public static class BookingConstants
    {
        public static readonly TimeOnly START_TIME = new TimeOnly(9, 0);
        public static readonly TimeOnly END_TIME = new TimeOnly(17, 0);
        public static readonly TimeOnly LAST_BOOKING_TIME = END_TIME.AddMinutes(-DURATION);
        public const int DURATION = 60;
        public const int MAX_SIMULTANEOUS_BOOKINGS = 4;
    }
}
