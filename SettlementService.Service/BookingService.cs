using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SettlementService.Constants;
using SettlementService.Domain.Abstractions;
using SettlementService.Domain.Entities;
using SettlementService.Interfaces;
using SettlementService.Interfaces.Model;

namespace SettlementService.Services
{
    public class BookingService : IBookingService
    {
        private IBookingRepository _bookingRepository;
        private IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddNewBooking(BookingModel booking)
        {
            await ValidateBooking(booking);
            Booking entity = _mapper.Map<Booking>(booking);
            return await _bookingRepository.CreateAsync(entity);
        }

        private async Task ValidateBooking(BookingModel booking)
        {
            if (string.IsNullOrEmpty(booking.ClientName))
            {
                throw new ArgumentException("Booking time is required");
            }

            if(await IsBookingAvailableAsync(booking.BookingTime))
            {
                throw new InvalidOperationException("Booking at this time is full");
            }

            if (IsBookingInWorkingHours(booking.BookingTime))
            {
                throw new ArgumentException("Booking time is not in the working hours");
            }
        }

        private async Task<bool> IsBookingAvailableAsync(TimeOnly bookingTime)
        {
            return (await _bookingRepository.GetByTimeAsync(bookingTime)).Count < BookingConstants.MAX_SIMULTANEOUS_BOOKINGS;
        }

        private bool IsBookingInWorkingHours(TimeOnly bookingTime)
        {
            return bookingTime >= BookingConstants.START_TIME && bookingTime <= BookingConstants.LAST_BOOKING_TIME;
        }
    }
}
