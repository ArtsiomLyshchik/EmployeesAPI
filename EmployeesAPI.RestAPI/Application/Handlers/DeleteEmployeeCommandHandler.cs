using EmployeesAPI.Data.Context;
using EmployeesAPI.RestAPI.Application.Commands;
using EmployeesAPI.RestAPI.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.RestAPI.Application.Handlers;

public class DeleteEmployeeCommandHandler: IRequestHandler<DeleteEmployeeCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteEmployeeCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
            
        if (employee is null)
        {
            throw new EntityNotFoundException("Could not find Employee with specified Id");
        }
        
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync(cancellationToken);
    }
}