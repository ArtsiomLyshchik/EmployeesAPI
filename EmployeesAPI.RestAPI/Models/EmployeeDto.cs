namespace EmployeesAPI.RestAPI.Models;

public record EmployeeDto(Guid Id, string Name, DateTime DateOfBirth, List<JobTitleDto> JobTitles);