using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementService.Domain.Exceptions
{
    public class NotInWorkingHoursException : IOException
    {
        public NotInWorkingHoursException(string message) : base(message)
        {
        }

    }
}
