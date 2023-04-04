using EmployeesAPI.Data.Context;
using EmployeesAPI.Data.Models;
using EmployeesAPI.RestAPI.Application.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.RestAPI.Application.Handlers;

public class CreateEmployeeCommandHandler: IRequestHandler<CreateEmployeeCommand, Guid>
{
    private readonly ApplicationDbContext _context;

    public CreateEmployeeCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee()
        {
            Name = request.Name,
            DateOfBirth = request.DateOfBirth,
        };

        var hasAnyJobTitle = request.JobTitleIds is not null && request.JobTitleIds.Any();

        if (hasAnyJobTitle)
        {
            var requestedJobTitles = request.JobTitleIds;

            var matchedJobTitles = await _context.JobTitles
                .Where(x => requestedJobTitles.Contains(x.Id))
                .ToListAsync(cancellationToken: cancellationToken);

                
            if (matchedJobTitles.Count != requestedJobTitles.Count)
            {
                throw new Exception("");
            }
                
            employee.JobTitles.AddRange(matchedJobTitles);
        }
            
        _context.Employees.Add(employee);
            
        await _context.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }
}