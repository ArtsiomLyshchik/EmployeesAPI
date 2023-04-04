using EmployeesAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeesAPI.Data.ModelConfigurations;

public class EmployeeEntityTypeConfiguration: IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder
            .ToTable("Employees")
            .HasKey(x => x.Id);

        builder.Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(255);
        builder.Property(x => x.DateOfBirth).HasColumnName("DateOfBirth");

        builder.HasMany(x => x.JobTitles).WithMany(x => x.Employees)
            .UsingEntity<EmployeeJobTitle>(
                l => l.HasOne<JobTitle>().WithMany().HasForeignKey(e => e.JobTitleId).OnDelete(DeleteBehavior.Restrict),
                r => r.HasOne<Employee>().WithMany().HasForeignKey(e => e.EmployeeId).OnDelete(DeleteBehavior.Cascade));
    }
}