using System.Net;

namespace Infrastructure.CustomException
{
    /// <summary>
    /// Api exception
    /// </summary>
    public abstract class ApiException : System.Exception
    {
        /// <summary>
        /// Http exception code
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="statusCode">Http status code</param>
        /// <param name="message">Message</param>
        /// <param name="ex">Exception</param>
        public ApiException(HttpStatusCode statusCode, string message, System.Exception ex)
            : base(message, ex)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Alternative constructro with message
        /// </summary>
        /// <param name="statusCode">Http status code</param>
        /// <param name="message">Message</param>
        public ApiException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Alternative constructor with only the http code
        /// </summary>
        /// <param name="statusCode">Http status code</param>
        public ApiException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}