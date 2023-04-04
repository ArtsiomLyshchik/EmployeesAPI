using EmployeesAPI.RestAPI.Application.Commands;
using FluentValidation;

namespace EmployeesAPI.RestAPI.Application.Validations;

public class UpdateJobTitleCommandValidator: AbstractValidator<UpdateJobTitleCommand>
{
    public UpdateJobTitleCommandValidator()
    {
        RuleFor(x => x.Name)
            .Custom((x, context) =>
            {
                if (x is null)
                {
                    return;
                }

                if (string.IsNullOrEmpty(x) || string.IsNullOrWhiteSpace(x))
                {
                    context.AddFailure("Name", "Name can not be empty");
                }

                if (x.Length > 255)
                {
                    context.AddFailure("Name", "Name exceeded max length 255 characters");
                }
            });

        RuleFor(x => x.Grade)
            .Custom((x, context) =>
            {
                if (x is null)
                {
                    return;
                }

                if (x < 1 || x > 15)
                {
                    context.AddFailure("Grade values must be in range between 1 and 15");
                }
            });
    }
}