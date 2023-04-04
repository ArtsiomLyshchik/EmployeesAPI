using EmployeesAPI.RestAPI.Models;
using MediatR;

namespace EmployeesAPI.RestAPI.Application.Queries;

public class GetJobTitleByIdQuery: IRequest<JobTitleDto>
{
    public Guid Id { get; set; }
}