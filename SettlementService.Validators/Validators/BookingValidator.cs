using SettlementService.Constants.Booking;
using SettlementService.Domain.Abstractions;
using SettlementService.DTO.Booking;

namespace SettlementService.Validators
{
    public class BookingValidator : IValidator<BookingDto>
    {
        private IBookingRepository _bookingRepository;
        
        public BookingValidator(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Result> Validate(BookingDto booking)
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

            if(!await IsBookingAvailableAsync(bookingTime))
            {
                return Result.Failure(BookingConstants.BookingFullError);
            }

            return Result.Success();
        }

        private bool IsBookingInWorkingHours(TimeOnly bookingTime)
        {
            return bookingTime >= BookingConstants.START_TIME && bookingTime <= BookingConstants.LAST_BOOKING_TIME;
        }
        private async Task<bool> IsBookingAvailableAsync(TimeOnly bookingTime)
        {
            return (await _bookingRepository.CountSimultanousBookings(bookingTime)) < BookingConstants.MAX_SIMULTANEOUS_BOOKINGS;
        }
    }
}
