using MediatR;

namespace EmployeesAPI.RestAPI.Application.Commands;

public class UpdateJobTitleCommand : IRequest
{
    public Guid? Id { get; set; }
    
    public string? Name { get; set; }

    public ushort? Grade { get; set; }
}