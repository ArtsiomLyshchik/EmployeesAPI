using EmployeesAPI.RestAPI.Common;
using EmployeesAPI.RestAPI.Exceptions;
using EmployeesAPI.RestAPI.Extensions;
using FluentValidation;
using MediatR;
using ValidationException = EmployeesAPI.RestAPI.Exceptions.ValidationException;

namespace EmployeesAPI.RestAPI.Application.Behaviors;

public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;    
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidatorBehavior<TRequest, TResponse>> logger)
    {        
        _validators = validators;
        _logger = logger;    
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {        
        var typeName = request.GetGenericTypeName();
        _logger.LogInformation("----- Validating command {CommandType}", typeName);
        var failures = _validators
            ?.Select(v => v.Validate(request))            ?
            .SelectMany(result => result.Errors)
            ?.Where(error => error != null)            ?
            .ToList();
        
        if (failures is null || !failures.Any())
        {            
            return await next().ConfigureAwait(false);
        }
        
        _logger.LogWarning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}", typeName, request, failures);
        throw new ValidationException($"Command Validation Errors for type {typeof(TRequest).Name}",            
            failures.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage)).ToList());
    }
}