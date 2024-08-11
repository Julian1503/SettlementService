using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementService.Domain.Abstractions
{

    public interface IValidator<T>
    {
        /// <summary>
        /// This method is used to validate an input, it could be Faliure or Success. 
        /// If it is Success, returns an value and if not an error.
        /// </summary>
        /// <param name="value">Input that will be validated</param>
        /// <returns>Result</returns>
        Task<Result> Validate(T value);
    }
}
