using System.Diagnostics.CodeAnalysis;

namespace SettlementService.DTO.Booking
{
    public class BookingDto
    {
        public Guid Id { get; set; }

        [NotNull]
        public string BookingTime { get; set; }

        [NotNull]
        public string Name { get; set; }

        public BookingDto()
        {
            Id = Guid.Empty;
            BookingTime = string.Empty;
            Name = string.Empty;
        }

        public BookingDto(Guid id, string bookingTime, string name)
        {
            Id = id;
            BookingTime = bookingTime;
            Name = name;
        }
    }
}
