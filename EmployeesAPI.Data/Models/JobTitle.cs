using EmployeesAPI.Data.ModelConfigurations;

namespace EmployeesAPI.Data.Models;

public class JobTitle
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public ushort Grade { get; set; }

    public List<Employee> Employees { get; } = new();
};