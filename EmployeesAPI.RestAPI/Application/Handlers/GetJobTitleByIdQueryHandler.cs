using EmployeesAPI.Data.Context;
using EmployeesAPI.RestAPI.Application.Queries;
using EmployeesAPI.RestAPI.Exceptions;
using EmployeesAPI.RestAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.RestAPI.Application.Handlers;

public class GetJobTitleByIdQueryHandler : IRequestHandler<GetJobTitleByIdQuery, JobTitleDto>
{
    private readonly ApplicationDbContext _context;
    
    public GetJobTitleByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<JobTitleDto> Handle(GetJobTitleByIdQuery request, CancellationToken cancellationToken)
    {
        var jobTitle = await _context.JobTitles
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        
        if (jobTitle is null)
        {
            throw new EntityNotFoundException("Could not find Job Title with specified Id");
        }

        var result = new JobTitleDto(
            jobTitle.Id,
            jobTitle.Name,
            jobTitle.Grade
        );

        return result;
    }
}