using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettlementService.Domain.Primitives;

namespace SettlementService.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public required string ClientName { get; set; }
        public required TimeOnly BookingTime { get; set; }
        public int Duration { get; set; }
    }
}
