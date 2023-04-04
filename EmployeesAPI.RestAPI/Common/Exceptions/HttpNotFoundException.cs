using System.Net;

namespace EmployeesAPI.RestAPI.Exceptions
{
    public class HttpNotFoundException : BaseHttpException
    {
        public HttpNotFoundException(string message)
            : base(HttpStatusCode.NotFound, message)
        {
        }

        public HttpNotFoundException()
            : base(HttpStatusCode.NotFound)
        {
        }
    }
}