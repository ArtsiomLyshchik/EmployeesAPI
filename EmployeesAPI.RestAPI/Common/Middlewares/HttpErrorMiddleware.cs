using System.Net;
using EmployeesAPI.RestAPI.Common;
using EmployeesAPI.RestAPI.Exceptions;

namespace EmployeesAPI.RestAPI.Middlewares;

public class HttpErrorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HttpErrorMiddleware> _logger;

    public HttpErrorMiddleware(RequestDelegate next, ILogger<HttpErrorMiddleware> logger)
    {
        _next = next;    
        _logger = logger;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context).ConfigureAwait(false);
        }
        catch (BaseHttpException ex)
        {
            var error = new HttpError(ex.Message, ex.Metadata);
            await WriteErrorAsync(context, (int)ex.StatusCode, error).ConfigureAwait(false);
        }
        catch (ValidationException ex)
        {
            var error = new HttpError(ex.Message,
                ex.ValidationErrors.ToDictionary(x => x.PropertyName, x => (object)x.ValidationMessage));
            await WriteErrorAsync(context, (int)HttpStatusCode.BadRequest, error).ConfigureAwait(false);
        }
        catch (EntityNotFoundException ex)
        {
            var error = new HttpError(ex.Message, null);
            await WriteErrorAsync(context, (int)HttpStatusCode.NotFound, error).ConfigureAwait(false);
        }
        catch (OperationNotAllowedException ex)
        {
            var error = new HttpError(ex.Message, null);
            await WriteErrorAsync(context, (int)HttpStatusCode.MethodNotAllowed, error).ConfigureAwait(false);
        }
        catch (Exception ex)
        {       
            _logger.LogError(ex, "Global handling exception");
            var error = new HttpError(ex.Message, null);        
            await WriteErrorAsync(context, (int)HttpStatusCode.InternalServerError, error).ConfigureAwait(false);
        }}
    private async Task WriteErrorAsync(HttpContext context, int statusCode, HttpError httpError)
    {    
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(httpError).ConfigureAwait(false);
    }
}