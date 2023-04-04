namespace EmployeesAPI.RestAPI.Common;

public record ValidationError(string PropertyName, string ValidationMessage);