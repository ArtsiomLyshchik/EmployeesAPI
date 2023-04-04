using EmployeesAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.Data.Context;

public class ApplicationDbContext: DbContext
{
    public DbSet<Employee> Employees { get; set; }

    public DbSet<JobTitle> JobTitles { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AppDomain.CurrentDomain.GetAssemblies()
            .First(x => x.GetName().Name!.Contains("EmployeesAPI.Data")));
        
        base.OnModelCreating(modelBuilder);
    }
}