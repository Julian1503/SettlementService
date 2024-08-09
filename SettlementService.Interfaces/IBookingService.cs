using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettlementService.Interfaces.Model;

namespace SettlementService.Interfaces
{
    public interface IBookingService
    {
        Task<Guid> AddNewBooking(BookingModel booking);
    }
}
