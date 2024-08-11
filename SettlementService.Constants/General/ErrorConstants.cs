using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettlementService.Domain.Abstractions;

namespace SettlementService.Constants.General
{
    public static class ErrorConstants
    {
        public readonly static Error InternalServerError = new Error("Internal Server Error.", "Internal Server Error.");
    }
}
