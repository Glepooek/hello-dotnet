using System.Net;

namespace RefitDemo.Common.Models
{
    /// <summary>
    /// Wraps an API operation outcome with success/failure state,
    /// error message, and HTTP status code.
    /// </summary>
    public class OperationResult<T>
    {
        public bool IsSuccess { get; init; }
        public T Data { get; init; }
        public string ErrorMessage { get; init; }
        public HttpStatusCode? StatusCode { get; init; }

        public static OperationResult<T> Success(T data) => new()
        {
            IsSuccess = true,
            Data = data
        };

        public static OperationResult<T> Failure(string message, HttpStatusCode? statusCode = null) => new()
        {
            IsSuccess = false,
            ErrorMessage = message,
            StatusCode = statusCode
        };
    }
}
