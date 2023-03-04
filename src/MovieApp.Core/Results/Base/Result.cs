namespace MovieApp.Core.Results.Base
{
    /// <summary>
    /// Defines generic result class.
    /// </summary>
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Result"/>.
        /// </summary>
        /// <param name="isSuccess">True if operation is successful, false if it failed.</param>
        /// <param name="error">If failed send the <see cref="Base.Error"/></param>
        protected Result(bool isSuccess, Error error)
        {
            //If isSuccess is true the error must be none 
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }

            //If isSuccess is false the error must have a value
            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        /// <summary>
        /// Create a new <see cref="Result"/> that has succeeded.
        /// </summary>
        /// <returns>A new <see cref="Result"/></returns>
        public static Result Success()
        {
            return new Result(true, Error.None);
        }

        /// <summary>
        /// Create a <see cref="Result"/> that has succeeded but that accapets a generic value into it.
        /// </summary>
        /// <typeparam name="TValue">Any time of result object.</typeparam>
        /// <param name="value">The value of the result object.</param>
        /// <returns>A new <see cref="Result"/> with a value</returns>
        public static Result<TValue> Success<TValue>(TValue value)
        {
            return new Result<TValue>(value, true, Error.None);
        }

        /// <summary>
        /// Create a new <see cref="Result"/> that has failed.
        /// </summary>
        /// <param name="error">The cause of the failure.</param>
        /// <returns>A new <see cref="Result"/></returns>
        public static Result Failure(Error error)
        {
            return new Result(false, error);
        }

        /// <summary>
        /// Create a new <see cref="Result"/> that has failed but that accapets a generic value into it.
        /// </summary>
        /// <typeparam name="TValue">Any time of result object.</typeparam>
        /// <param name="error">The cause of the failure.</param>
        /// <returns>A new <see cref="Result"/> with a value</returns>
        public static Result<TValue> Failure<TValue>(Error error)
        {
            return new Result<TValue>(default, false, error);
        }
    }

    /// <summary>
    /// Defines generic result class that accapets a generic value.
    /// </summary>
    /// <typeparam name="TValue">Any time of result object.</typeparam>
    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        /// <summary>
        /// Initializes a new instance of <see cref="Result"/> that accapets a generic value into it.
        /// </summary>
        /// <param name="value">The generic value.</param>
        /// <param name="isSuccess">True if operation is successful, false if it failed.</param>
        /// <param name="error">If failed send the <see cref="Base.Error"/></param>
        internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _value = value;
        }

        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The value of the failure result can not be accessed");

        public static implicit operator Result<TValue>(TValue value)
        {
            return Success(value);
        }
    }

    /// <summary>
    /// Defines generic error that might happen.
    /// </summary>
    public sealed class Error
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Error"/>.
        /// </summary>
        /// <param name="code">HTTP status code.</param>
        /// <param name="message">Massage of the error.</param>
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }
        public string Message { get; }

        public static readonly Error None = new(string.Empty, string.Empty);

        public static readonly Error InternalServerError = new("500", "Internal Server Error");
    }
}