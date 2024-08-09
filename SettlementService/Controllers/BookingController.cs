using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SettlementService.Interfaces;

namespace SettlementService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("AddBooking")]
        public IActionResult AddBooking()
        {
           return Ok();
        }
    }
}
