using EmployeesAPI.Data.Context;
using EmployeesAPI.RestAPI.Application.Commands;
using EmployeesAPI.RestAPI.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.RestAPI.Application.Handlers;

public class UpdateJobTitleCommandHandler: IRequestHandler<UpdateJobTitleCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateJobTitleCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(UpdateJobTitleCommand request, CancellationToken cancellationToken)
    {
        var jobTitleToPatch = await _context.JobTitles
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        
        if (jobTitleToPatch is null)
        {
            throw new EntityNotFoundException("Could not found Job Title with provided Id");
        }
        
        jobTitleToPatch.Name = request.Name ?? jobTitleToPatch.Name;
        jobTitleToPatch.Grade = request.Grade ?? jobTitleToPatch.Grade;
        
        _context.Entry(jobTitleToPatch).State = EntityState.Modified;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}