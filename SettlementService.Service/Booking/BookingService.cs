using AutoMapper;
using SettlementService.Constants.Booking;
using SettlementService.Domain.Abstractions;
using SettlementService.Domain.Entities;
using SettlementService.DTO.Booking;
using SettlementService.Interfaces.Booking;
using SettlementService.Mapper;
using SettlementService.Validators;

namespace SettlementService.Services.Booking
{ 
    public class BookingService : IBookingService
    {
        private IBookingRepository _bookingRepository;
        private IMapper _mapper;
        private IValidator<BookingDto> _validator;

        public BookingService(IBookingRepository bookingRepository, IValidator<BookingDto> validator)
        {
            _bookingRepository = bookingRepository;
            _validator = validator;
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
            _mapper = mapperConfig.CreateMapper();
        }

        public async Task<Result<Guid>> AddNewBooking(BookingDto booking)
        {
            Result validation = await _validator.Validate(booking);

            if(validation.isFailure)
            {
                return Result.Failure<Guid>(validation.Error);
            }

            Domain.Entities.Booking entity = _mapper.Map<Domain.Entities.Booking>(booking);
            Guid idBookingAdded = await _bookingRepository.CreateAsync(entity);
            return Result.Success(idBookingAdded);
        }
    }
}
