using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementService.Domain.Abstractions
{
    /// <summary>
    /// Represents the result of an operation, which can either be a success or a failure.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        public bool isSuccess { get; }

        /// <summary>
        /// Indicates whether the operation was a failure.
        /// </summary>
        public bool isFailure => !isSuccess;

        /// <summary>
        /// Gets the error associated with the result, if any.
        /// </summary>
        public Error Error { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="isSuccess">Indicates if the result is successful.</param>
        /// <param name="error">The error associated with the result, if any.</param>
        /// <exception cref="ArgumentException">Thrown when the state of the result is invalid.</exception>
        public Result(bool isSuccess, Error error)
        {
            if (IsStateInvalid(isSuccess, error))
            {
                throw new ArgumentException("Invalid Error", nameof(error));
            }

            this.isSuccess = isSuccess;
            this.Error = error;
        }

        /// <summary>
        /// Creates a successful <see cref="Result"/>.
        /// </summary>
        /// <returns>A <see cref="Result"/> indicating success.</returns>
        public static Result Success() =>
            new Result(true, Error.None);

        /// <summary>
        /// Creates a failure <see cref="Result"/> with the specified error.
        /// </summary>
        /// <param name="error">The error associated with the failure.</param>
        /// <returns>A <see cref="Result"/> indicating failure.</returns>
        public static Result Failure(Error error) =>
            new Result(false, error);


        /// <summary>
        /// Creates a successful <see cref="Result{T}"/> with the specified value.
        /// </summary>
        /// <typeparam name="T">The type of the value associated with the result.</typeparam>
        /// <param name="value">The value associated with the success.</param>
        /// <returns>A <see cref="Result{T}"/> indicating success.</returns>
        public static Result<T> Success<T>(T value) =>
            new Result<T>(value, true, Error.None);

        /// <summary>
        /// Creates a failure <see cref="Result{T}"/> with the specified error.
        /// </summary>
        /// <typeparam name="T">The type of the value that would have been associated with the result.</typeparam>
        /// <param name="error">The error associated with the failure.</param>
        /// <returns>A <see cref="Result{T}"/> indicating failure.</returns>
        public static Result<T> Failure<T>(Error error) =>
            new Result<T>(default, false, error);

        /// <summary>
        /// Validates the state of the result to ensure it is consistent.
        /// </summary>
        /// <param name="isSuccess">Indicates if the result is successful.</param>
        /// <param name="error">The error associated with the result.</param>
        /// <returns>True if the state is invalid; otherwise, false.</returns>
        private bool IsStateInvalid(bool isSuccess, Error error)
        {
            return (isSuccess && error != Error.None) || (!isSuccess && error == Error.None);
        }
    }

    /// <summary>
    /// Represents the result of an operation that returns a value, which can either be a success or a failure.
    /// </summary>
    /// <typeparam name="T">The type of the value associated with the result.</typeparam>
    public class Result<T> : Result
    {
        private readonly T? _value;

        public Result(T? value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the value associated with the result.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the result is a failure.</exception>
        [NotNull]
        public T Value => isSuccess
            ? _value!
            : throw new InvalidOperationException("The value of a failure result can't be accessed.");

        /// <summary>
        /// Implicitly converts a value of type <typeparamref name="T"/> to a successful <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="value">The value to be converted.</param>
        /// <returns>A successful <see cref="Result{T}"/> containing the value.</returns>
        public static implicit operator Result<T>(T? value) => value is not null
            ? Success(value)
            : Failure<T>(Error.NullValue);

        /// <summary>
        /// Creates a validation failure <see cref="Result{T}"/> with the specified error.
        /// </summary>
        /// <param name="error">The error associated with the validation failure.</param>
        /// <returns>A <see cref="Result{T}"/> indicating validation failure.</returns>
        public static Result<T> ValidationFailure(Error error) =>
            new(default, false, error);
    }

}
