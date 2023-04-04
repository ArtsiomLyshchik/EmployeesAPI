using EmployeesAPI.Data.Context;
using EmployeesAPI.RestAPI.Application.Commands;
using EmployeesAPI.RestAPI.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.RestAPI.Application.Handlers;

public class UpdateEmployeeCommandHandler: IRequestHandler<UpdateEmployeeCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateEmployeeCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employeeToPatch = await _context.Employees.Include(x => x.JobTitles)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        
        if (employeeToPatch is null)
        {
            throw new EntityNotFoundException("Could not found Employee with provided Id");
        }
        
        employeeToPatch.Name = request.Name ?? employeeToPatch.Name;
        employeeToPatch.DateOfBirth = request.DateOfBirth ?? employeeToPatch.DateOfBirth;
        
        var hasAnyJobTitle = request.JobTitleIds is not null && request.JobTitleIds.Any();
        
        if (hasAnyJobTitle)
        {
            var requestedJobTitles = request.JobTitleIds;
        
            var matchedJobTitles = await _context.JobTitles
                .Where(x => requestedJobTitles.Contains(x.Id))
                .ToListAsync(cancellationToken: cancellationToken);
        
            if (matchedJobTitles.Count != requestedJobTitles.Count)
            {
                throw new EntityNotFoundException("Could not find Job Titles with provided Id to assign to Employee");
            }
                
            employeeToPatch.JobTitles.Clear();
            employeeToPatch.JobTitles.AddRange(matchedJobTitles);
        }
            
        _context.Entry(employeeToPatch).State = EntityState.Modified;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}