using EmployeesAPI.RestAPI.Common;

namespace EmployeesAPI.RestAPI.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(string message, IReadOnlyCollection<ValidationError> validationErrors)
            : base(message)
        {
            ValidationErrors = validationErrors;
        }

        public IReadOnlyCollection<ValidationError> ValidationErrors { get; }
    }
}