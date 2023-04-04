using EmployeesAPI.Data.Context;
using EmployeesAPI.RestAPI.Application.Queries;
using EmployeesAPI.RestAPI.Exceptions;
using EmployeesAPI.RestAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.RestAPI.Application.Handlers;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
{
    private readonly ApplicationDbContext _context;
    
    public GetEmployeeByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
            .Include(x => x.JobTitles)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        
        if (employee is null)
        {
            throw new EntityNotFoundException("Could not find Employee with specified Id");
        }

        var result = new EmployeeDto(
            employee.Id,
            employee.Name,
            employee.DateOfBirth,
            employee.JobTitles.Select(x => new JobTitleDto(x.Id, x.Name, x.Grade)).ToList()
        );

        return result;
    }
}