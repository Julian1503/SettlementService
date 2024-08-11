using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SettlementService.Domain.Abstractions
{
    /// <summary>
    /// Represents an error in the system, inheriting from <see cref="ProblemDetails"/>.
    /// This class is used to standardize error messages returned by my API.
    /// </summary>
    public class Error : ProblemDetails
    {
        public Error(string title, string detail, int? statusCode = null)
        {
            Title = title;
            Detail = detail;
            Status = statusCode;
        }

        /// <summary>
        /// Represents a non-error state (i.e., no error).
        /// </summary>
        public static readonly Error None = new(string.Empty, string.Empty);

        /// <summary>
        /// Represents a "Bad Request" error with a "Null value" message.
        /// </summary>
        public static readonly Error NullValue = new("Bad Request", "Null value", 400);

        /// <summary>
        /// Serializes the current instance to a JSON string.
        /// </summary>
        /// <returns>A JSON string representation of the error.</returns>
        override public string ToString() => JsonSerializer.Serialize(this);
    } 
}
