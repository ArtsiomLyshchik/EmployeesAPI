using EmployeesAPI.RestAPI.Models;
using MediatR;

namespace EmployeesAPI.RestAPI.Application.Commands;

public class CreateEmployeeCommand: IRequest<Guid>
{
    public string Name { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public List<Guid>? JobTitleIds { get; set; }
}