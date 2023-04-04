using MediatR;

namespace EmployeesAPI.RestAPI.Application.Commands;

public class DeleteEmployeeCommand: IRequest
{
    public Guid Id { get; set; }
}