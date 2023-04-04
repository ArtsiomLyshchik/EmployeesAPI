using EmployeesAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeesAPI.Data.ModelConfigurations;

public class JobTitleEntityTypeConfiguration: IEntityTypeConfiguration<JobTitle>
{
    public void Configure(EntityTypeBuilder<JobTitle> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(255);
        builder.Property(x => x.Grade).HasColumnName("Grade");
    
        builder.ToTable(name:"JobTitle", buildAction: tableBuilder =>
            tableBuilder.HasCheckConstraint("Grade", "Grade BETWEEN 1 AND 15"));
    }
}