using System.Net;

namespace EmployeesAPI.RestAPI.Exceptions
{
    public class HttpBadRequestException : BaseHttpException
    {
        public HttpBadRequestException(string message)
            : base(HttpStatusCode.BadRequest, message)
        {
        }

        public HttpBadRequestException()
            : base(HttpStatusCode.BadRequest)
        {
        }
    }
}