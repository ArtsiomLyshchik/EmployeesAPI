using EmployeesAPI.Data.Context;
using EmployeesAPI.Data.Models;
using EmployeesAPI.RestAPI.Application.Commands;
using EmployeesAPI.RestAPI.Models;
using MediatR;

namespace EmployeesAPI.RestAPI.Application.Handlers;

public class CreateJobTitleCommandHandler: IRequestHandler<CreateJobTitleCommand, Guid>
{
    private readonly ApplicationDbContext _context;
    
    public CreateJobTitleCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> Handle(CreateJobTitleCommand request, CancellationToken cancellationToken)
    {
        var jobTitle = new JobTitle
        {
            Name = request.Name,
            Grade = request.Grade
        };

        _context.JobTitles.Add(jobTitle);
        await _context.SaveChangesAsync(cancellationToken);

        return jobTitle.Id;
    }
}