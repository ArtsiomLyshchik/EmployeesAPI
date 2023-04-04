namespace EmployeesAPI.RestAPI.Exceptions;

public class OperationNotAllowedException: BaseException
{
    public OperationNotAllowedException()
    {
    }

    public OperationNotAllowedException(string message) : base(message)
    {
    }

    public OperationNotAllowedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}