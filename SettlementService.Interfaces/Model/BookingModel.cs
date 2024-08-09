using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementService.Interfaces.Model
{
    public class BookingModel
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public TimeOnly BookingTime { get; set; }
    }
}
