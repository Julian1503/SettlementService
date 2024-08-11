using Microsoft.AspNetCore.Mvc;
using SettlementService.API.Response.Booking;
using SettlementService.Domain.Abstractions;
using SettlementService.DTO.Booking;
using SettlementService.Interfaces.Booking;

namespace SettlementService.API.Controllers
{
    /// <summary>
    /// API controller for managing bookings.
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Creates a new booking based on the provided booking data.
        /// </summary>
        /// <param name="bookingDto">The booking data transfer object containing booking details.</param>
        /// <returns>An action result indicating the outcome of the booking creation.</returns>
        
        [HttpPost]
        public async Task<IActionResult> AddNewBooking(BookingDto bookingDto)
        {
            // Call the booking service to add a new booking
            Result<Guid> result = await _bookingService.AddNewBooking(bookingDto);

            // Check if the booking was successfully created
            if (result.isSuccess)
            {
                // Return a successful response with the new booking ID
                BookingAddResponse response = new BookingAddResponse(result.Value);
                return Ok(response);
            }
            else
            {
                // Return a problem response with error details
                return Problem(
                    title: result.Error.Title,
                    detail: result.Error.Detail,
                    statusCode: result.Error.Status
                );
            }
        }
    }
}
