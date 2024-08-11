using AutoMapper;
using SettlementService.Constants.Booking;
using SettlementService.Domain.Abstractions;
using SettlementService.Domain.Entities;
using SettlementService.DTO.Booking;
using SettlementService.Interfaces.Booking;
using SettlementService.Mapper;
using SettlementService.Validators;

namespace SettlementService.Services.BookingService
{
    public class BookingService : IBookingService
    {
        private IBookingRepository _bookingRepository;
        private IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
            _mapper = mapperConfig.CreateMapper();
        }

        public async Task<Result> AddNewBooking(BookingDto booking)
        {
            BookingValidator.Validate(booking);
            Booking entity = _mapper.Map<Booking>(booking);
            return Result.Success(await _bookingRepository.CreateAsync(entity));
        }

        private async Task<bool> IsBookingAvailableAsync(TimeOnly bookingTime)
        {
            return (await _bookingRepository.GetByTimeAsync(bookingTime)).Count < BookingConstants.MAX_SIMULTANEOUS_BOOKINGS;
        }
    }
}
