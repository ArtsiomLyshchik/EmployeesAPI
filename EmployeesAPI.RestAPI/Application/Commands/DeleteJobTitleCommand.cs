using MediatR;

namespace EmployeesAPI.RestAPI.Application.Commands;

public class DeleteJobTitleCommand: IRequest
{
    public Guid? Id { get; set; }
}