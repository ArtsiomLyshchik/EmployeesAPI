namespace EmployeesAPI.RestAPI.Common;

public record HttpError(string Message, IDictionary<string, object>? Data);