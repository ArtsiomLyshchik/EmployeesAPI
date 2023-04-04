using MediatR;

namespace EmployeesAPI.RestAPI.Application.Commands;

public class UpdateEmployeeCommand: IRequest
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public DateTime? DateOfBirth { get; set; }
    
    public List<Guid>? JobTitleIds { get; set; }
}