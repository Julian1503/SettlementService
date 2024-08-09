namespace SettlementService.Interfaces.Model
{
    public class BookingModel
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public TimeOnly BookingTime { get; set; }
    }
}
