using SettlementService.Domain.Primitives;

namespace SettlementService.Domain.Entities
{
    /// <summary>
    /// Represents a Booking in the database
    /// </summary>
    public class Booking : BaseEntity
    {
        public required string ClientName { get; set; }
        public required TimeOnly BookingTime { get; set; }
        public int Duration { get; set; }
    }
}
