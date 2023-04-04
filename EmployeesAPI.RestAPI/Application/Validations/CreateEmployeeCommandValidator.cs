using EmployeesAPI.RestAPI.Application.Commands;
using FluentValidation;

namespace EmployeesAPI.RestAPI.Application.Validations;

public class CreateEmployeeCommandValidator: AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name can not be empty")
            .MaximumLength(255)
            .WithMessage("Name exceeded max length 255 characters");
    }
}