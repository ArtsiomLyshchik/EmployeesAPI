using EmployeesAPI.Data.Context;
using EmployeesAPI.RestAPI.Application.Commands;
using EmployeesAPI.RestAPI.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.RestAPI.Application.Handlers;

public class DeleteJobTitleCommandHandler: IRequestHandler<DeleteJobTitleCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteJobTitleCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteJobTitleCommand request, CancellationToken cancellationToken)
    {
        var jobTitle = await _context.JobTitles.Include(x => x.Employees).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
            
        if (jobTitle is null)
        {
            throw new EntityNotFoundException("Could not find Job Title with specified Id");
        }

        var doesAnyEmployeeAssignedToTitle = jobTitle.Employees.Any();

        if (doesAnyEmployeeAssignedToTitle)
        {
            throw new OperationNotAllowedException("Could not delete Job Title with existing referencing from Employees");
        }
        
        _context.JobTitles.Remove(jobTitle);
        await _context.SaveChangesAsync(cancellationToken);
    }
}