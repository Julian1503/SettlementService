using SettlementService.Constants.Booking;
using SettlementService.Domain.Abstractions;
using SettlementService.DTO.Booking;

namespace SettlementService.Validators
{
    public static class BookingValidator
    {
        public static Result Validate(BookingDto booking)
        {
            if (string.IsNullOrEmpty(booking.Name))
            {
                return Result.Failure(BookingConstants.ClientNameRequiredError);
            }
            TimeOnly bookingTime;
            if (!TimeOnly.TryParse(booking.BookingTime, out bookingTime))
            {
                return Result.Failure(BookingConstants.InvalidTimeFormatError);
            }

            if (!IsBookingInWorkingHours(bookingTime))
            {
                return Result.Failure(BookingConstants.NotWorkingHoursError);
            }

            return Result.Success();
        }

        private static bool IsBookingInWorkingHours(TimeOnly bookingTime)
        {
            return bookingTime >= BookingConstants.START_TIME && bookingTime <= BookingConstants.LAST_BOOKING_TIME;
        }
    }
}
