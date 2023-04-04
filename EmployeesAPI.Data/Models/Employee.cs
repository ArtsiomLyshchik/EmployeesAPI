namespace EmployeesAPI.Data.Models;

public class Employee
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public DateTime DateOfBirth { get; set; }

    public List<JobTitle> JobTitles { get; } = new();
}
