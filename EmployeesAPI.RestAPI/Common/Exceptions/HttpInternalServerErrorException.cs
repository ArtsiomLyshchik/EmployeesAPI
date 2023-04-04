using System.Net;

namespace EmployeesAPI.RestAPI.Exceptions
{
    public class HttpInternalServerErrorException : BaseHttpException
    {
        public HttpInternalServerErrorException(string message)
            : base(HttpStatusCode.InternalServerError, message)
        {
        }

        public HttpInternalServerErrorException()
            : base(HttpStatusCode.InternalServerError)
        {
        }
    }
}