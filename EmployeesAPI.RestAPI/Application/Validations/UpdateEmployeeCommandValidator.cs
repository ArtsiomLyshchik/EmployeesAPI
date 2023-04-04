using EmployeesAPI.RestAPI.Application.Commands;
using FluentValidation;

namespace EmployeesAPI.RestAPI.Application.Validations;

public class UpdateEmployeeCommandValidator: AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id can not be empty");

        RuleFor(c => c.Name)
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
    }
}