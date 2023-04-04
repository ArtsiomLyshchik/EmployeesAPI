using EmployeesAPI.RestAPI.Application.Commands;
using FluentValidation;

namespace EmployeesAPI.RestAPI.Application.Validations;

public class CreateJobTitleCommandValidator: AbstractValidator<CreateJobTitleCommand>
{
    public CreateJobTitleCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name can not be empty")
            .MaximumLength(255)
            .WithMessage("Name exceeded max length 255 characters");

        RuleFor(x => x.Grade)
            .GreaterThanOrEqualTo((ushort)1)
            .WithMessage("Grade can not be less than 1")
            .LessThanOrEqualTo((ushort)15)
            .WithMessage("Grade can not be greater than 15");
    }
}