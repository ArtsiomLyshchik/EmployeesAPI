using EmployeesAPI.RestAPI.Models;
using MediatR;

namespace EmployeesAPI.RestAPI.Application.Commands;

public class CreateJobTitleCommand: IRequest<Guid>
{
    public string Name { get; set; }

    public ushort Grade { get; set; }
}