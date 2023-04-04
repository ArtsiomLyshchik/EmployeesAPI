using System.Net;

namespace EmployeesAPI.RestAPI.Exceptions
{
    public class BaseHttpException : BaseException
    {
        public BaseHttpException(int statusCode, string message)
            : this((HttpStatusCode) statusCode, message)
        {
        }

        public BaseHttpException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public BaseHttpException(HttpStatusCode statusCode)
            : base()
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }

        public Dictionary<string, object> Metadata { get; } = new();
    }
}