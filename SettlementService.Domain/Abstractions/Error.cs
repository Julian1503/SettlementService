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
    public class Error : ProblemDetails
    {
        public Error(string title, string detail, int? statusCode = null)
        {
            Title = title;
            Detail = detail;
            Status = statusCode;
        }

        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Bad Request", "Null value", 400);

        override public string ToString() => JsonSerializer.Serialize(this);
    }

    public class Result
    {
        public bool isSuccess { get; }
        public bool isFailure => !isSuccess;
        public Error Error { get; }

        public Result(bool isSuccess, Error error)
        {
            if (IsStateInvalid(isSuccess, error))
            {
                throw new ArgumentException("Invalid Error", nameof(error));
            }

            this.isSuccess = isSuccess;
            this.Error = error;
        }
        public static Result Success() => 
            new Result(true, Error.None);

        public static Result Failure(Error error) => 
            new Result(false, error);

        public static Result<T> Success<T>(T value) => 
            new Result<T>(value, true, Error.None);

        public static Result<T> Failure<T>(Error error) => 
            new Result<T>(default, false, error);
        private bool IsStateInvalid(bool isSuccess, Error error)
        {
            return (isSuccess && error != Error.None) || (!isSuccess && error == Error.None);
        }
    }

    public class Result<T> : Result
    {
        private readonly T? _value;

        public Result(T? value,  bool isSuccess, Error error) 
            : base(isSuccess, error) {
            _value = value;
        }

        [NotNull]
        public T Value => isSuccess 
            ? _value! 
            : throw new InvalidOperationException("The value of a failure result can't be accessed.");

        public static implicit operator Result<T>(T? value) => value is not null 
            ? Success(value) 
            : Failure<T>(Error.NullValue);
        
        public static Result<T> ValidationFailure(Error error) => 
            new(default, false, error);
    }

 
}
