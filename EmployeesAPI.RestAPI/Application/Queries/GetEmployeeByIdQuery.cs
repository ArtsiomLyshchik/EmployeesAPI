using EmployeesAPI.RestAPI.Models;
using MediatR;

namespace EmployeesAPI.RestAPI.Application.Queries;

public class GetEmployeeByIdQuery: IRequest<EmployeeDto>
{
    public Guid Id { get; set; }
}