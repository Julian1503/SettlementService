using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SettlementService.API.AutoMapperDto;
using SettlementService.API.Dto;
using SettlementService.Interfaces.Booking;
using SettlementService.Interfaces.Model;
using SettlementService.Mapper;

namespace SettlementService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private IBookingService _bookingService;
        private IMapper _mapper;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<ProfileDtoConfig>());
            _mapper = config.CreateMapper();
        }

        [HttpPost("AddBooking")]
        public async Task<IActionResult> AddBooking(BookingDto bookingDto)
        {
            BookingModel bookingModel = _mapper.Map<BookingModel>(bookingDto);
            Guid newBookingId = await _bookingService.AddNewBooking(bookingModel);
            return Ok(newBookingId);
        }
    }
}
